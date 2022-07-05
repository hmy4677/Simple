namespace Simple.Application.Payment.Model;

/// <summary>
/// 支付宝请求公共参数
/// </summary>
public class AliPayRequestCommon
{
    /// <summary>
    /// 支付宝分配给开发者的应用ID
    /// </summary>
    public string app_id { get; set; }

    /// <summary>
    /// 接口名称
    /// </summary>
    public string method { get; set; } = "alipay.trade.wap.pay";

    /// <summary>
    /// 仅支持JSON
    /// </summary>
    public string format { get; set; } = "JSON";

    /// <summary>
    /// 返回的url HTTP/HTTPS开头字符串
    /// </summary>
    public string return_url { get; set; }

    /// <summary>
    /// 请求使用的编码格式
    /// </summary>
    public string charset { get; set; } = "utf-8";

    /// <summary>
    /// 商户生成签名字符串所使用的签名算法类型
    /// </summary>
    public string sign_type { get; set; } = "RSA2";

    /// <summary>
    /// 商户请求参数的签名串
    /// </summary>
    public string sign { get; set; }

    /// <summary>
    /// 发送请求的时间格式"yyyy-MM-dd HH:mm:ss
    /// </summary>
    public string timestamp { get; set; }

    /// <summary>
    /// 调用的接口版本
    /// </summary>
    public string version { get; set; } = "1.0";

    /// <summary>
    /// 支付宝服务器主动通知商户服务器里指定的页面http/https路径
    /// </summary>
    public string notify_url { get; set; }

    /// <summary>
    /// 用户授权
    /// </summary>
    public string app_auth_token { get; set; }

    /// <summary>
    /// 请求参数的集合，最大长度不限，除公共参数外所有请求参数都必须放在这个参数中传递，
    /// </summary>
    public string biz_content { get; set; }
}