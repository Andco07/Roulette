using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruleta.Application;
using Ruleta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ruleta.Abstractions;
using Ruleta.DTOs;

namespace Ruleta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuletaController : ControllerBase
    {
        IAplication<Roulette> _roulette;
        public RuletaController (IAplication<Roulette> roulette)
        {
            _roulette = roulette;
        }
       [HttpPost]
        public async Task<IActionResult> CrearRuleta()
        {
            var f = new Roulette()
            {
                IsOpen = false,
                OpenedAt = null,
                ClosedAt = null
            };
            await _roulette.SaveAsync(f);
            return Ok(f);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roulette.GetAllAsync());
        }
        /// <summary>
        /// Open the rulette id
        /// </summary>
        /// <param name="id">rulette id</param>
        /// <returns></returns>
        [HttpPut("{id}/open")]
        public async Task<IActionResult> Open([FromRoute(Name = "id")] int id) 
        {

            var roulette = await _roulette.GetByIdAsync(id);

            if (roulette == null)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La ruleta no existe"
                });
            }

            if (roulette.OpenedAt != null)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La ruleta ya está abierta"
                });
            }
            roulette.Id = id;
            roulette.OpenedAt = DateTime.Now;
            roulette.IsOpen = true;
            await _roulette.SaveAsync(roulette);
            return Ok(roulette);
        }
        /// <summary>
        /// Closes bets on a rulette
        /// </summary>
        /// <param name="id"> rulette id</param>
        /// <returns></returns>
        [HttpPut("{id}/close")]
        public async Task<IActionResult> Close([FromRoute(Name = "id")] int id)
        {
            var roulette = await _roulette.GetByIdAsync(id);

            if (roulette == null)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La ruleta no existe"
                });
            }

            if (roulette.ClosedAt != null)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La ruleta ya está cerrada"
                });
            }
            roulette.Id = id;
            roulette.ClosedAt = DateTime.Now;
            roulette.IsOpen = false;
            await _roulette.SaveAsync(roulette);
            return Ok(roulette);
        }
        /// <summary>
        /// It lets make a bet between [0.5 and 10000, red or black]
        /// </summary>
        /// <param name="UserId">user id</param>
        /// <param name="id"> roulette id</param>
        /// <param name="request">piece number, [0,36] number [37=> red, 38=> black] </param>
        /// <returns></returns>
        [HttpPost("{id}/bet")]
        public async Task<IActionResult> Bet([FromHeader(Name = "user-id")] string UserId, [FromRoute(Name = "id")] int id,
            [FromBody] BetRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Bad Request"
                });
            }
            if (request.money > 10000 || request.money < 1)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La apuesta debe estar entre 1 y 10000"
                });
            }
            var roulette = await _roulette.GetByIdAsync(id);
            if (roulette == null)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "La ruleta no existe"
                });
            }
            if (roulette.IsOpen == false)
            {
                return BadRequest(new
                {
                    error = true,
                    msg = "Esta ruleta ya cerró sus apuestas"
                });
            }
            double value = 0d;
            roulette.board[request.position].TryGetValue(UserId, out value);
            roulette.board[request.position].Remove(UserId + "");
            roulette.board[request.position].TryAdd(UserId + "", value + request.money);
            await _roulette.SaveAsync(roulette);
            return Ok(roulette);
           

        }

    }
}
