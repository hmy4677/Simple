namespace Simple.Application.Payment.Enum;

public enum TradeStatus
{
    /// <summary>
    /// 支付成功
    /// </summary>
    SUCCESS,

    /// <summary>
    /// 转入退款
    /// </summary>
    REFUND,

    /// <summary>
    /// 未支付
    /// </summary>
    NOTPAY,

    /// <summary>
    /// 已关闭
    /// </summary>
    CLOSED,

    /// <summary>
    /// 已撤销（仅付款码支付会返回）
    /// </summary>
    REVOKED,

    /// <summary>
    /// 用户支付中（仅付款码支付会返回）
    /// </summary>
    USERPAYIN,

    /// <summary>
    /// 支付失败（仅付款码支付会返回）
    /// </summary>
    PAYERROR
}