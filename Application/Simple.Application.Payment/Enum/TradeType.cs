namespace Simple.Application.Payment.Enum;

/// <summary>
/// 微信支付类型
/// </summary>
public enum TradeType
{
    /// <summary>
    /// 公众号支付
    /// </summary>
    JSAPI,

    /// <summary>
    /// 扫码支付
    /// </summary>
    NATIVE,

    /// <summary>
    /// APP支付
    /// </summary>
    APP,

    /// <summary>
    /// 付款码支付
    /// </summary>
    MICROPAY,

    /// <summary>
    /// H5支付
    /// </summary>
    MWEB,

    /// <summary>
    /// 刷脸支付
    /// </summary>
    FACEPAY
}