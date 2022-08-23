using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.Options;
using Simple.Application.Payment.Interface;
using Simple.Application.Payment.Option;
using System.Text.Json;

namespace Simple.Application.Payment.Implement;

/// <summary>
/// 支付宝支付服务
/// </summary>
public class AliPay : IAliPay, ITransient
{
    private readonly AliPayOptions _aliPayOptions;
    private readonly string _appid;
    private const string _apiurl = "https://openapi.alipay.com/gateway.do";

    public AliPay(IOptions<AliPayOptions> aliPayOptions)
    {
        _aliPayOptions = aliPayOptions.Value;
        _appid = _aliPayOptions.AppId;
    }

    /// <summary>
    /// 手机网页支付
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <param name="subject">商品名称</param>
    /// <param name="amount"><金额/param>
    /// <param name="return_url">返回url</param>
    public AlipayTradeWapPayResponse WapPay(string outTradeNo, string subject, decimal amount, string return_url)
    {
        var privateKey = GetKeyFromFile(_aliPayOptions.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_aliPayOptions.AliPublicKeyPath);
        var client = new DefaultAopClient(_apiurl, _appid, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeWapPayRequest();
        request.SetNotifyUrl(_aliPayOptions.PayNotifyUrl);
        request.SetReturnUrl(return_url);
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNo },
                { "total_amount", amount },
                { "subject", subject },
                { "product_code", "QUICK_WAP_WAY" }
            };
        request.BizContent = JsonSerializer.Serialize(bizContent);
        var response = client.pageExecute(request);
        return response;
    }

    /// <summary>
    /// 支付账单查询
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <returns></returns>
    public AlipayTradeQueryResponse QueryPay(string outTradeNo)
    {
        var privateKey = GetKeyFromFile(_aliPayOptions.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_aliPayOptions.AliPublicKeyPath);
        var client = new DefaultAopClient(_apiurl, _appid, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeQueryRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNo }
            };
        //bizContent.Add("trade_no", "2014112611001004680073956707");

        request.BizContent = JsonSerializer.Serialize(bizContent);
        var response = client.Execute(request);
        return response;
    }

    /// <summary>
    /// 支付宝退款
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <param name="refundId">退款id</param>
    /// <param name="refundAmount">退款金额</param>
    /// <param name="reason">退款原因</param>
    /// <returns></returns>
    public AlipayTradeRefundResponse Refund(string outTradeNo, string refundId, decimal refundAmount, string reason)
    {
        var privateKey = GetKeyFromFile(_aliPayOptions.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_aliPayOptions.AliPublicKeyPath);
        var client = new DefaultAopClient(_apiurl, _appid, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeRefundRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no",  outTradeNo},
                { "refund_amount", refundAmount },
                { "out_request_no", refundId },
                {"refund_reason",reason }
            };

        //// 返回参数选项，按需传入
        //List<string> queryOptions = new List<string>();
        //queryOptions.Add("refund_detail_item_list");
        //bizContent.Add("query_options", queryOptions);

        request.BizContent = JsonSerializer.Serialize(bizContent);
        var response = client.Execute(request);
        return response;
    }

    /// <summary>
    /// 关闭订单
    /// </summary>
    /// <param name="outTradeNO">商户单号</param>
    /// <returns>关闭响应</returns>
    public AlipayTradeCloseResponse Close(string outTradeNO)
    {
        var privateKey = GetKeyFromFile(_aliPayOptions.PrivateKeyPath);
        var publicKey = GetKeyFromFile(_aliPayOptions.AliPublicKeyPath);
        var client = new DefaultAopClient(_apiurl, _appid, privateKey, "json", "1.0", "RSA2", publicKey, "UTF-8", false);
        var request = new AlipayTradeCloseRequest();
        var bizContent = new Dictionary<string, object>
            {
                { "out_trade_no", outTradeNO }
            };
        request.BizContent = JsonSerializer.Serialize(bizContent);
        var response = client.Execute(request);
        return response;
    }

    /// <summary>
    /// 从文件中获取密钥
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>文件内容</returns>
    private static string GetKeyFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw Oops.Oh("密钥文件不存在");
        }
        using var fileStream = new FileStream(filePath, FileMode.Open);
        var reader = new StreamReader(fileStream);
        var key = reader.ReadToEnd();
        return key;
    }
}