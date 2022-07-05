using SqlSugar;
using SqlSugar.IOC;

namespace Simple.Core.Sugar;

public class Repository<T> : SimpleClient<T> where T : class, new()
{
    public Repository(ISqlSugarClient? context = null) : base(context)//注意这里要有默认值等于null
    {
        //base.Context = context;//ioc注入的对象
        base.Context = DbScoped.SugarScope;//  SqlSugar.Ioc这样写
        //base.Context = DbHelper.GetDbInstance();// 当然也可以手动去赋值
    }
}