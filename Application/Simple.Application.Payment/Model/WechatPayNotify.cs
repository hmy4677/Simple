namespace Simple.Application.Payment.Model;

/// <summary>
/// 微信回调通知model
/// </summary>
public class WechatPayNotify
{
    /// <summary>
    /// 通知ID
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// 通知创建时间
    /// </summary>
    public string create_time { get; set; }

    /// <summary>
    /// 通知类型
    /// </summary>
    public string event_type { get; set; }

    /// <summary>
    /// 通知数据类型
    /// </summary>
    public string resource_type { get; set; }

    /// <summary>
    /// 回调摘要
    /// </summary>
    public string summary { get; set; }

    /// <summary>
    /// 通知数据
    /// </summary>
    public ResourceInfo resource { get; set; }
}

/// <summary>
/// 通知数据
/// </summary>
public class ResourceInfo
{
    /// <summary>
    /// 加密算法类型
    /// </summary>
    public string algorithm { get; set; }

    /// <summary>
    /// 数据密文
    /// </summary>
    public string ciphertext { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public string associated_data { get; set; }

    /// <summary>
    /// 原始类型
    /// </summary>
    public string original_type { get; set; }

    /// <summary>
    /// 随机串
    /// </summary>
    public string nonce { get; set; }
}