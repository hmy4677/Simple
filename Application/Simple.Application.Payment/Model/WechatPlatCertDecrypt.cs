namespace Simple.Application.Payment.Model;

public class WechatPlatCertDecrypt
{
    public string serial_no { get; set; }
    public string effective_time { get; set; }
    public string expire_time { get; set; }
    public string decrypttext { get; set; }
}