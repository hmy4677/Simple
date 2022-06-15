using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple.Services.System.Interface;
using Simple.Services.System.Model.User;

namespace Simple.Controllers.System
{
  /// <summary>
  /// 用户控制器
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserController(IUserService service, IHttpContextAccessor httpContextAccessor)
    {
      _service = service;
      _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<dynamic> Login([FromBody] LoginInput input)
    {
      var user = await _service.UserLogin(input);
      var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
              {
                {"UserId",user.Id },
                {"Account",user.Account },
              });
      var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken);
      _httpContextAccessor.HttpContext.Response.Headers["access-token"] = accessToken;
      _httpContextAccessor.HttpContext.Response.Headers["x-access-token"] = refreshToken;
      return user;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public bool Del(long id)
    {
      return _service.DeleteUser(id);
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> AddNewUser([FromBody] UserAddInput input)
    {
      return await _service.AddUser(input);
    }

    /// <summary>
    /// 修改用户
    /// </summary>
    /// <param name="id"></param>
    /// <param name="info"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<int> UpdateUser(long id, [FromBody] UserInfo info)
    {
      return await _service.UpdateUser(id, info);
    }

    [AllowAnonymous]
    [HttpGet("redis_get")]
    public async Task<string> RedisGet()
    {
      var data = await _service.RedisGet();
      return data;
    }

    [AllowAnonymous]
    [HttpGet("redis_set")]
    public async Task RedisSet()
    {
      await _service.RedisSet();
    }
  }
}