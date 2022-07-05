namespace Simple.Application.Payment.Model;

/// <summary>
/// 微信小程序签名包
/// </summary>
public class WeAppSignInfo
{
    public string appId { get; set; }
    public int timeStamp { get; set; }
    public string nonceStr { get; set; }
    public string package { get; set; }
    public string signType { get; set; } = "RSA";
    public string paySign { get; set; }
    public string prepay_id { get; set; }
}