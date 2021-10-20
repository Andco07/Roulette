using Ruleta.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace Ruleta.Cache
{
    public static class CacheExtension
    {
        public static IServiceCollection UseRedisCache(this IServiceCollection service, IConfiguration config)
        {
            if (!config.GetSection("Endpoint").Exists())
            {
                throw new ArgumentNullException("You must create Cache:Endpoint section in appsettings.json");
            }

            service.Configure<CacheSettings>(options => config.Bind(options));
            ConnectionMultiplexer cm = ConnectionMultiplexer.Connect(config.GetSection("Endpoint").Value);
            service.AddSingleton<IConnectionMultiplexer>(cm);
            service.AddScoped(typeof(ICacheService<>), typeof(CacheService<>));


            return service;
        }

    }
}
