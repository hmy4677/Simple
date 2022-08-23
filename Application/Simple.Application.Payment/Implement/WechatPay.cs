using Furion.DependencyInjection;
using Furion.RemoteRequest.Extensions;
using Microsoft.Extensions.Options;
using Simple.Application.Payment.Interface;
using Simple.Application.Payment.Model;
using Simple.Application.Payment.Option;
using System.Text.Json;

namespace Simple.Application.Payment.Implement;

/// <summary>
/// 微信支付服务
/// </summary>
public class WechatPay : IWechatPay, ITransient
{
    private readonly WechatPayOptions _wechatPayOptions;
    private readonly ISecurity _security;

    public WechatPay(IOptions<WechatPayOptions> wechatPayOptions, ISecurity security)
    {
        _wechatPayOptions = wechatPayOptions.Value;
        _security = security;
    }


    /// <summary>
    /// 生成预付订单
    /// </summary>
    /// <param name="amount">金额(单位:分)</param>
    /// <param name="des">商品描述</param>
    /// <param name="outTradeNO">商户单号</param>
    /// <param name="openId">支付者openid</param>
    /// <returns>预付单号</returns>
    public async Task<string> PrepayTransaction(int amount, string des, string outTradeNO, string openId)
    {
        var url = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
        var body = new WechatPayPrepayRequest
        {
            appid = _wechatPayOptions.Appid,
            mchid = _wechatPayOptions.Mchid,
            amount = new AmountPre { total = amount },
            description = des,
            notify_url = _wechatPayOptions.PayNotifyUrl,
            out_trade_no = outTradeNO,
            payer = new PayerInfo { openid = openId }
        };
        var bodyjson = JsonSerializer.Serialize(body);
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "POST",
            bodyjson = bodyjson
        });
        var response = await url
            .SetHeaders(new WechatPayRequestHead { Authorization = authorization })
            .SetBody(bodyjson)
            .PostAsAsync<WechatPayPrepay>();
        return response.prepay_id;
    }

    /// <summary>
    /// 支付查询(微信支付号)
    /// </summary>
    /// <param name="transactionId">微信支付号</param>
    /// <returns>查询结果</returns>
    public async Task<WechatPayQueryResponse> QueryByTransactionId(string transactionId)
    {
        var url = $"https://api.mch.weixin.qq.com/v3/pay/transactions/id/{transactionId}?mchid={_wechatPayOptions.Mchid}";
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "GET"
        });
        var response = await url.SetHeaders(new WechatPayRequestHead
        {
            Authorization = authorization
        }).GetAsAsync<WechatPayQueryResponse>();

        return response;
    }

    /// <summary>
    /// 支付查询(商户单号)
    /// </summary>
    /// <param name="outTradeNO">商户单号</param>
    /// <returns></returns>
    public async Task<WechatPayQueryResponse> QueryByOutTradeNo(string outTradeNO)
    {
        var url = $"https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{outTradeNO}?mchid={_wechatPayOptions.Mchid}";
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "GET"
        });

        var response = await url.SetHeaders(new WechatPayRequestHead
        {
            Authorization = authorization
        }).GetAsAsync<WechatPayQueryResponse>();
        return response;
    }

    /// <summary>
    /// 关闭订单
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <returns>是否成功</returns>
    public async Task<bool> CloseByOutTradeNo(string outTradeNo)
    {
        var url = $"https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{outTradeNo}/close";
        var body = new { mchid = _wechatPayOptions.Mchid };
        var bodyjson = JsonSerializer.Serialize(body);
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "POST",
            bodyjson = bodyjson
        });
        var response = await url.SetHeaders(new WechatPayRequestHead { Authorization = authorization })
            .SetBody(bodyjson)
            .PostAsync();
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            return true;
        return false;
    }

    /// <summary>
    /// 退款
    /// </summary>
    /// <param name="totalAmount">订单总金额</param>
    /// <param name="refundAmount">退款金额</param>
    /// <param name="refundNo">退款单号</param>
    /// <param name="outTradeNo">商户单号</param>
    /// <param name="transactionId">微信支付单号</param>
    /// <param name="reason">退款原因</param>
    /// <returns>退款结果</returns>
    public async Task<WechatPayRefundResponse> Refund(int totalAmount, int refundAmount, string refundNo, string outTradeNo)
    {
        var url = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds";
        var body = new WechatPayRefundRequest
        {
            out_refund_no = refundNo,
            out_trade_no = outTradeNo,
            amount = new RefundAmount
            {
                total = totalAmount,
                refund = refundAmount,
            },
            //transaction_id = transactionId,
            //notify_url = _wechatPayOptions.RefundNotifyUrl,
        };
        var bodyjson = JsonSerializer.Serialize(body);
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "POST",
            bodyjson = bodyjson
        });
        var response = await url.SetHeaders(new WechatPayRequestHead { Authorization = authorization })
            .SetBody(bodyjson)
            .PostAsAsync<WechatPayRefundResponse>();
        return response;
    }

    /// <summary>
    /// 退款查询
    /// </summary>
    /// <param name="refundNo">退款单号</param>
    /// <returns>查询结果</returns>
    public async Task<WechatPayRefundQueryResponse> QueryRefund(string refundNo)
    {
        var url = $"https://api.mch.weixin.qq.com/v3/refund/domestic/refunds/{refundNo}";
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "GET"
        });
        var response = await url.SetHeaders(new WechatPayRequestHead
        {
            Authorization = authorization
        }).GetAsAsync<WechatPayRefundQueryResponse>();
        return response;
    }

    /// <summary>
    /// 获取支付平台证书(验签用)
    /// </summary>
    /// <returns></returns>
    public async Task<List<WechatPlatCertDecrypt>> GetCertificates()
    {
        var url = "https://api.mch.weixin.qq.com/v3/certificates";
        var authorization = _security.BuildAuthorization(new SecurityInfo
        {
            url = TrimUrl(url),
            method = "GET"
        });
        var response = await url.SetHeaders(new WechatPayRequestHead
        {
            Authorization = authorization
        }).GetAsAsync<PlactCertResponse>();


        var list = new List<WechatPlatCertDecrypt>();
        foreach (var item in response.data)
        {
            list.Add(new WechatPlatCertDecrypt
            {
                serial_no = item.serial_no,
                effective_time = item.effective_time,
                expire_time = item.expire_time,
                decrypttext = _security.AesGcmDecrypt(
                    item.encrypt_certificate.associated_data,
                    item.encrypt_certificate.nonce,
                    item.encrypt_certificate.ciphertext)
            });
        }

        return list;

    }


    private static string TrimUrl(string url)
    {
        return url.Replace("https://api.mch.weixin.qq.com", string.Empty).TrimEnd('/');
    }

}
