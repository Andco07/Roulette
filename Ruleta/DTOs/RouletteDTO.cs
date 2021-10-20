using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta.DTOs
{
    public class RouletteDTO
    {
        public int Id { get; internal set; }
        public bool IsOpen { get; set; } = false;

        public DateTime? OpenedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public IDictionary<string, double>[] board { get; set; } = new IDictionary<string, double>[39];

        public RouletteDTO()
        {
            this.Init();
        }

        private void Init()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = new Dictionary<string, double>();
            }
        }

    }
}
