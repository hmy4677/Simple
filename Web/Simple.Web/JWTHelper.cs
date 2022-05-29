using Furion.Authorization;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;

namespace Simple.Web
{

  /// <summary>
  /// JWT 授权自定义处理程序
  /// </summary>
  public class JwtHandler : AppAuthorizeHandler
  {
    /// <summary>
    /// 添加自动刷新收取逻辑
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
      // 自动刷新 token
      if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
      {
        await AuthorizeHandleAsync(context);
      }
      else context.Fail();    // 授权失败
    }

    /// <summary>
    /// 验证管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
      return Task.FromResult(true);
    }
  }
}