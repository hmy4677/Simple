namespace Simple.Application.Payment.Model;

/// <summary>
/// 微信退款查询响应
/// </summary>
public class WechatPayRefundQueryResponse
{
    /// <summary>
    /// 微信支付退款单号
    /// </summary>
    public string refund_id { get; set; }

    /// <summary>
    /// 商户退款单号
    /// </summary>
    public string out_refund_no { get; set; }

    /// <summary>
    /// 微信支付订单号
    /// </summary>
    public string transaction_id { get; set; }

    /// <summary>
    /// 商户订单号
    /// </summary>
    public string out_trade_no { get; set; }

    /// <summary>
    /// 退款渠道
    /// </summary>
    public string channel { get; set; }

    /// <summary>
    /// 退款入账账户
    /// </summary>
    public string user_received_account { get; set; }

    /// <summary>
    /// 退款成功时间
    /// </summary>
    public string success_time { get; set; }

    /// <summary>
    /// 退款创建时间
    /// </summary>
    public string create_time { get; set; }

    /// <summary>
    /// 退款状态
    /// </summary>
    public string status { get; set; }

    /// <summary>
    /// 资金账户
    /// </summary>
    public string funds_account { get; set; }

    /// <summary>
    /// 金额信息
    /// </summary>
    public RefundAmount amount { get; set; }
}