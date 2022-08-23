using Aop.Api.Response;

namespace Simple.Application.Payment.Interface;

/// <summary>
/// 支付宝支付接口
/// </summary>
public interface IAliPay
{
    /// <summary>
    /// 手机网页支付
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <param name="subject">商品名称</param>
    /// <param name="amount">金额</param>
    /// <param name="return_url">返回url</param>
    /// <returns></returns>
    AlipayTradeWapPayResponse WapPay(string outTradeNo, string subject, decimal amount, string return_url);

    /// <summary>
    /// 查询支付
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <returns></returns>
    AlipayTradeQueryResponse QueryPay(string outTradeNo);

    /// <summary>
    /// 关闭订单
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <returns></returns>
    AlipayTradeCloseResponse Close(string outTradeNo);

    /// <summary>
    /// 退款
    /// </summary>
    /// <param name="outTradeNo">商户单号</param>
    /// <param name="refundId">退款id</param>
    /// <param name="refundAmount">退款金额</param>
    /// <param name="reason">退款原因</param>
    /// <returns></returns>
    AlipayTradeRefundResponse Refund(string outTradeNo, string refundId, decimal refundAmount, string reason);
}