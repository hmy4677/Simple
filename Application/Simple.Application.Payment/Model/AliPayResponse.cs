namespace Simple.Application.Payment.Model;

/// <summary>
/// 支付宝支付响应参数
/// </summary>
internal class AliPayResponse
{
    /// <summary>
    /// 商户网站唯一订单号
    /// </summary>
    public string out_trade_no { get; set; }

    /// <summary>
    /// 该交易在支付宝系统中的交易流水号
    /// </summary>
    public string trade_no { get; set; }

    /// <summary>
    /// 该笔订单的资金总额，单位为人民币（元）
    /// </summary>
    public decimal total_amount { get; set; }

    /// <summary>
    /// 收款支付宝账号对应的支付宝唯一用户号。
    /// </summary>
    public string seller_id { get; set; }

    /// <summary>
    /// 商户原始订单号
    /// </summary>
    public string merchant_order_no { get; set; }
}