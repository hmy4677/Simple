namespace Simple.Application.Payment.Model;

/// <summary>
/// 下单请求body
/// </summary>
public class WechatPayPrepayRequest
{
    /// <summary>
    /// 应用id
    /// </summary>
    public string appid { get; set; }

    /// <summary>
    /// 直连商户号
    /// </summary>
    public string mchid { get; set; }

    /// <summary>
    /// 商品描述
    /// </summary>
    public string description { get; set; }

    /// <summary>
    /// 商户订单号
    /// </summary>
    public string out_trade_no { get; set; }

    /// <summary>
    /// 通知地址
    /// </summary>
    public string notify_url { get; set; }

    /// <summary>
    /// 订单金额信息
    /// </summary>
    public AmountPre amount { get; set; }

    /// <summary>
    /// 支付者信息
    /// </summary>
    public PayerInfo payer { get; set; }
}

public class AmountPre
{
    /// <summary>
    /// 货币类型
    /// </summary>
    public string currency { get; set; } = "CNY";

    /// <summary>
    /// 总金额(单位:分)
    /// </summary>
    public int total { get; set; }
}

/// <summary>
/// 订单金额信息
/// </summary>
public class AmountInfo : AmountPre
{

    /// <summary>
    /// 支付者支付的金额
    /// </summary>
    public int? payer_total { get; set; }

    /// <summary>
    /// 用户支付货币
    /// </summary>
    public string? payer_currency { get; set; }
}

/// <summary>
/// 支付者信息
/// </summary>
public class PayerInfo
{
    /// <summary>
    /// openid
    /// </summary>
    public string openid { get; set; }
}