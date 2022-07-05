using System;
using Furion.DependencyInjection;
using Simple.Data.Entity;
using Simple.Services.DbInitial.Interface;
using SqlSugar;
using SqlSugar.IOC;

namespace Simple.Services.DbInitial.Implement
{
    public class DbInitial : IDbInitial, ITransient
    {
        private readonly SqlSugarProvider _db;

        public DbInitial()
        {
            _db = DbScoped.SugarScope.GetConnection("db1");
        }

        /// <summary>
        /// 建库建表
        /// </summary>
        public void CreateDbAndTable()
        {
            _db.DbMaintenance.CreateDatabase("SimpleDB");
            //单个建表
            //_db.CodeFirst.SetStringDefaultLength(50).InitTables<UserEntity>();

            //批量建表//任意实体类中的类
            var types = typeof(UserEntity).Assembly.GetTypes()
            .Where(p => p.FullName.Contains("Entity"))//命名空间过滤
            .ToArray();
            _db.CodeFirst.SetStringDefaultLength(50).InitTables(types);
        }

    }
}

