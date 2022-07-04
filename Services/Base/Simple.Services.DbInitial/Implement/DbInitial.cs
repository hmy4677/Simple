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
      //_db.CodeFirst.SetStringDefaultLength(50).InitTables<UserEntity>();//单个建表

      var types = typeof(UserEntity).Assembly.GetTypes()//任意实体类中的类
                                                        //.Where(p => p.FullName.Contains("OrmTest."))//命名空间过滤，当然你也可以写其他条件过滤
        .ToArray();
      _db.CodeFirst.SetStringDefaultLength(50).InitTables(types);//根据types创建表
    }

  }
}

