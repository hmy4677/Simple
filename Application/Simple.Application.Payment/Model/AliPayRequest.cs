namespace Simple.Application.Payment.Model;

/// <summary>
/// 支付宝支付请求参数
/// </summary>
public class AliPayRequest
{
    /// <summary>
    /// 商户单号
    /// </summary>
    public string out_trade_no { get; set; }

    /// <summary>
    /// 订单金额
    /// </summary>
    public decimal total_amount { get; set; }

    /// <summary>
    /// 订单标题
    /// </summary>
    public string subject { get; set; }

    /// <summary>
    /// 销售产品码
    /// </summary>
    public string product_code { get; set; } = "QUICK_WAP_WAY";

    /// <summary>
    /// 用户授权接口
    /// </summary>
    public string? auth_token { get; set; }

    /// <summary>
    /// 用户付款中途退出返回商户网站的地址
    /// </summary>
    public string quit_url { get; set; }
}