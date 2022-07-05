using Simple.Application.Payment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.Payment.Interface;

public interface IWechatPay
{
    /// <summary>
    /// 生成预付订单
    /// </summary>
    /// <param name="amount">金额(单位:分)</param>
    /// <param name="des">商品描述</param>
    /// <param name="tradeNO">商户单号</param>
    /// <param name="openId">支付者openid</param>
    /// <returns>预付单号</returns>
    Task<string> PrepayTransaction(int amount, string des, string tradeNO, string openId);

    /// <summary>
    /// 支付查询(微信支付单号)
    /// </summary>
    /// <param name="transactionId">微信支付单位号</param>
    /// <returns>查询结果</returns>
    Task<WechatPayQueryResponse> QueryByTransactionId(string transactionId);

    /// <summary>
    /// 支付查询(商户单号)
    /// </summary>
    /// <param name="tradeNO">商户单号</param>
    /// <returns></returns>
    Task<WechatPayQueryResponse> QueryByOutTradeNo(string tradeNO);

    /// <summary>
    /// 关闭订单
    /// </summary>
    /// <param name="tradeNo">商户单号</param>
    /// <returns>是否成功</returns>
    Task<bool> CloseByOutTradeNo(string tradeNo);

    /// <summary>
    /// 退款
    /// </summary>
    /// <param name="totalAmount">订单总金额</param>
    /// <param name="refundAmount">退款金额</param>
    /// <param name="refundNo">退款单号</param>
    /// <param name="tradeNo">商户单号</param>
    /// <returns>退款结果</returns>
    Task<WechatPayRefundResponse> Refund(int totalAmount, int refundAmount, string refundNo, string tradeNo);

    /// <summary>
    /// 退款查询
    /// </summary>
    /// <param name="refundNO">退款单号</param>
    /// <returns>查询结果</returns>
    Task<WechatPayRefundQueryResponse> QueryRefund(string refundNO);

    /// <summary>
    /// 获取支付平台证书(验签用)
    /// </summary>
    /// <returns></returns>
    Task<List<WechatPlatCertDecrypt>> GetCertificates();
}
