using Furion.ConfigurableOptions;

namespace Simple.Application.Payment.Option;

/// <summary>
/// 微信支付选项
/// </summary>
public class WechatPayOptions : IConfigurableOptions
{
    public string Appid { get; set; }
    public string Mchid { get; set; }
    public string PayNotifyUrl { get; set; }
    public string RefundNotifyUrl { get; set; }
    public string Key { get; set; }
    public string CertSerialno { get; set; }
    public string PrivateKeyPath { get; set; }
    public string PublicKeyPath { get; set; }
    public string Appsecret { get; set; }
    public string PlatCertPath { get; set; }
}