using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Simple.Core.Redis
{
    public class SimpleRedis : ISimpleRedis
    {
        private readonly IDatabase _db;

        public SimpleRedis(IDatabase db)
        {
            _db = db;
        }

        //写入string[]
        public async Task<int> SetArray(string key, string[] array)
        {
            var count = 0;
            foreach (var item in array)
            {
                await _db.ListRightPushAsync(key, item.ToString());
                count++;
            }
            return count;
        }

        //读取string[]
        public async Task<string?[]?> GetArray(string key, long startIndex, long endIndex)
        {
            var data = await _db.ListRangeAsync(key, startIndex, endIndex);
            return data.ToStringArray();
        }
    }
}
