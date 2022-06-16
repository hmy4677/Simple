using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Simple.Core.Redis
{
    public static class RedisConnection
    {
        public static IServiceCollection AddRedisConnection(this IServiceCollection service)
        {
            var redis = ConnectionMultiplexer.Connect(App.Configuration.GetConnectionString("Redis")).GetDatabase();
            service.AddSingleton(redis);
            return service;
        }
    }
}
