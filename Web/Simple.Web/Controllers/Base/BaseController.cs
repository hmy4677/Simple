using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Simple.Services.DbInitial.Interface;

namespace Simple.Web.Controllers.Base;
/// <summary>
/// 基础控制器
/// </summary>
[AllowAnonymous]
[Route("api/[controller]")]
public class BaseController : Controller
{
  private readonly IDbInitial _dbInitial;
  private readonly IMemoryCache _memory;
  private const string _timeCacheKey = "timeKey";

  public BaseController(IDbInitial dbInitial, IMemoryCache memory)
  {
    _dbInitial = dbInitial;
    _memory = memory;
  }

  /// <summary>
  /// 初始化数据库
  /// </summary>
  [HttpGet("init_db")]
  public void InitDB()
  {
    _dbInitial.CreateDbAndTable();
  }

  [HttpGet("time")]
  public string UseMemoryCache()
  {
    return _memory.GetOrCreate(_timeCacheKey, entry =>
    {
      entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(100);
      return DateTime.Now.ToString("T");
    });
  }
}