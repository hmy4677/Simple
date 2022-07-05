namespace Simple.Application.Payment.Model;

public class PlactCertResponse
{
    public List<PlatCertInfo> data { get; set; }
}

public class PlatCertInfo
{
    public string serial_no { get; set; }
    public string effective_time { get; set; }
    public string expire_time { get; set; }
    public EncryptCert encrypt_certificate { get; set; }
}

public class EncryptCert
{
    public string algorithm { get; set; }
    public string nonce { get; set; }
    public string associated_data { get; set; }
    public string ciphertext { get; set; }
}