namespace Simple.Application.Payment.Model;

/// <summary>
/// 微信退款请求信息
/// </summary>
public class WechatPayRefundRequest
{
    /// <summary>
    /// 微信单号(与商户单号二选一)
    /// </summary>
    //public string transaction_id { get; set; }

    /// <summary>
    /// 商户单号(与微信单号二选一)
    /// </summary>
    public string out_trade_no { get; set; }

    /// <summary>
    /// 退款单号
    /// </summary>
    public string out_refund_no { get; set; }

    /// <summary>
    /// 退款原因
    /// </summary>
    //public string reason { get; set; }

    /// <summary>
    /// 通知url
    /// </summary>
    //public string notify_url { get; set; }

    /// <summary>
    /// 金额信息
    /// </summary>
    public RefundAmount amount { get; set; }
}

public class RefundAmount
{
    /// <summary>
    /// 退款金额
    /// </summary>
    public int refund { get; set; }

    /// <summary>
    /// 订单总金额
    /// </summary>
    public int total { get; set; }

    /// <summary>
    /// 币种
    /// </summary>
    public string currency { get; set; } = "CNY";

    /// <summary>
    /// 用户支付金额
    /// </summary>
    //public int payer_total { get; set; }

    /// <summary>
    /// 用户退款金额
    /// </summary>
    //public int payer_refund { get; set; }

    /// <summary>
    /// 应结退款金额
    /// </summary>
    //public int settlement_refund { get; set; }

    /// <summary>
    /// 应结订单金额
    /// </summary>
    //public int settlement_total { get; set; }

    /// <summary>
    /// 优惠退款金额
    /// </summary>
    //public int discount_refund { get; set; }
}