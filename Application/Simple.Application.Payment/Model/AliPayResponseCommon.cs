namespace Simple.Application.Payment.Model;

/// <summary>
/// 支付宝响应公共参数
/// </summary>
public class AliPayResponseCommon
{
    /// <summary>
    /// 网关返回码
    /// </summary>
    public string code { get; set; }

    /// <summary>
    /// 网关返回码描述,
    /// </summary>
    public string msg { get; set; }

    /// <summary>
    /// 业务返回码
    /// </summary>
    public string sub_code { get; set; }

    /// <summary>
    /// 业务返回码描述
    /// </summary>
    public string sub_msg { get; set; }

    /// <summary>
    /// 签名
    /// </summary>
    public string sign { get; set; }
}