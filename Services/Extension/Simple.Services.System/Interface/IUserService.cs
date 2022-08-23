using Simple.Data.Entity;
using Simple.Services.System.Model.User;

namespace Simple.Services.System.Interface;

/// <summary>
/// 用户服务
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<UserInfo> UserLogin(LoginInput input);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户id</param>
    /// <returns></returns>
    bool DeleteUser(long id);

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<bool> AddUser(UserAddInput input);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="id">用户id</param>
    /// <param name="info"></param>
    Task<bool> UpdateUser(long id, UserInfo info);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id">用户id</param>
    /// <returns>用户信息</returns>
    Task<UserEntity> GetUserEntity(long id);
}