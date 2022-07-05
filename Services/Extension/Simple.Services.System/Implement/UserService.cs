using Furion.DataEncryption.Extensions;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Mapster;
using Microsoft.Extensions.Caching.Distributed;
using Simple.Core.Sugar;
using Simple.Data.Entity;
using Simple.Services.System.Interface;
using Simple.Services.System.Model.User;
using SqlSugar;
using SqlSugar.IOC;
using StackExchange.Redis;
using Yitter.IdGenerator;

namespace Simple.Services.System.Implement;

/// <summary>
/// 用户服务
/// </summary>
public class UserService : Repository<UserEntity>, IUserService, ITransient
{
    private readonly SqlSugarProvider _db;
    private readonly IDatabase _redis;

    public UserService(IDistributedCache cache, IDatabase redis)
    {
        _db = DbScoped.SugarScope.GetConnection("db1");
        _redis = redis;
    }

    //登录
    public async Task<UserInfo> UserLogin(LoginInput input)
    {
#if DEBUG

        var admin = await _db.Queryable<UserEntity>().FirstAsync(p => p.Account == "admin");
        if (admin == null)
        {
            var newadmin = new UserEntity
            {
                Id = YitIdHelper.NextId(),
                Account = "admin",
                Name = "Admin",
                Password = "123456".ToMD5Encrypt(),
                Status = 1,
                CreateTime = DateTime.Now
            };
            await _db.Insertable(newadmin).IgnoreColumns(true).ExecuteCommandAsync();
        }
#endif
        var user = await _db.Queryable<UserEntity>().FirstAsync(p => p.Account == input.Account);
        _ = user ?? throw Oops.Oh("Account is Error");
        if (!input.Password.ToMD5Compare(user.Password)) throw Oops.Oh("Passwork is Error");
        var userinfo = user.Adapt<UserInfo>();
        return userinfo;
    }

    //删除用户
    public bool DeleteUser(long id)
    {
        return base.DeleteById(id);//仓储中自带方法，下同
    }

    //新增用户
    public async Task<bool> AddUser(UserAddInput input)
    {
        var newUser = input.Adapt<UserEntity>();
        newUser.CreateTime = DateTime.Now;
        newUser.Id = YitIdHelper.NextId();
        newUser.Password = input.Password.ToMD5Encrypt();

        return await base.InsertAsync(newUser);
    }

    //更新用户
    public async Task<int> UpdateUser(long id, UserInfo info)
    {
        var user = await base.GetByIdAsync(id);
        user = info.Adapt(user);
        user.UpdateTime = DateTime.Now;
        return await AsUpdateable(user).IgnoreColumns(true).ExecuteCommandAsync();
    }
}