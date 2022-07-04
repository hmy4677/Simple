using Furion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar.IOC;

namespace Simple.Core.Sugar
{
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    public static class SugarConnection
    {
        public static IServiceCollection AddDBConnection(this IServiceCollection service)
        {
            var iocList = new List<IocConfig>
            {
                new IocConfig
                {
                    ConfigId="db1",//id自定义唯一
                    ConnectionString=App.Configuration.GetConnectionString("SimpleDB"),//appsettion 连接字符串
                    DbType=IocDbType.SqlServer,         
                    IsAutoCloseConnection=true
                },
                //还可以添加其它数据库配置
                //。。。
            };

            service.AddSqlSugar(iocList);
            return service;
        }
    }
}
