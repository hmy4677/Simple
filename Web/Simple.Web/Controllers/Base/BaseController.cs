using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Simple.Application.Payment.Interface;
using Simple.Application.Payment.Model;
using Simple.Services.DbInitial.Interface;
using System.Text.Json;

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
    private readonly ISecurity _security;
    private const string _timeCacheKey = "timeKey";

    public BaseController(IDbInitial dbInitial, IMemoryCache memory, ISecurity security)
    {
        _dbInitial = dbInitial;
        _memory = memory;
        _security = security;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    [HttpGet("init_db")]
    public void InitDB()
    {
        _dbInitial.CreateDbAndTable();
    }

    /// <summary>
    /// 内存缓存使用示例
    /// </summary>
    /// <returns></returns>
    [HttpGet("time")]
    public string UseMemoryCache()
    {
        return _memory.GetOrCreate(_timeCacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(100);
            return DateTime.Now.ToString("T");
        });
    }

    /// <summary>
    /// 支付宝付款通知回调
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("alipay_notify")]
    public async Task<IActionResult> AliNotify()
    {
        var sArray = GetRequestPost();

        string out_trade_no = sArray["out_trade_no"];
        string trade_no = sArray["trade_no"];
        string total_amount = sArray["total_amount"];

        if (sArray.Count != 0)
        {
            //验签
            bool flag = _security.VerifyAli(sArray);
            if (!flag)
            {
                return BadRequest();
            }
            //验签成功操作
            //...

            return Ok();
        }
        return BadRequest();
    }

    /// <summary>
    /// 微信付款通知回调
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("wechat_notify")]
    public async Task<IActionResult> WechatNotify()
    {
        //获取body
        var smr = new StreamReader(Request.Body);
        var bodyjson = await smr.ReadToEndAsync();
        if (string.IsNullOrEmpty(bodyjson))
        {
            var error = new
            {
                code = "FAIL",
                message = "内容为空"
            };
            return BadRequest(JsonSerializer.Serialize(error));
        }
        //验签
        var signature = Request.Headers["Wechatpay-Signature"];
        var stamp = Request.Headers["Wechatpay-Timestamp"];
        var nonce = Request.Headers["Wechatpay-Nonce"];
        var verify = _security.VerifyWechat(signature, stamp, nonce, bodyjson);
        if (!verify)
        {
            var error = new
            {
                code = "FAIL",
                message = "验签失败"
            };
            return BadRequest(JsonSerializer.Serialize(error));
        }

        //解密
        var notify = JsonSerializer.Deserialize<WechatPayNotify>(bodyjson);
        var decrypt =JsonSerializer.Deserialize<WechatPayQueryResponse>(
            _security.AesGcmDecrypt(notify.resource.associated_data, notify.resource.nonce, notify.resource.ciphertext));

        if (decrypt.trade_state_desc != "支付成功")
        {
            var error = new
            {
                code = "FAIL",
                message = decrypt.trade_state_desc
            };
            return BadRequest(JsonSerializer.Serialize(error));
        }
        //更新订单数据
        //...

        return Ok();
    }

    [NonAction]
    private Dictionary<string, string> GetRequestPost()
    {
        var sArray = new Dictionary<string, string>();
        var coll = Request.Form;
        foreach (var item in coll)
        {
            sArray.Add(item.Key, item.Value);
        }
        return sArray;
    }
}