using Furion.ConfigurableOptions;

namespace Simple.Application.Payment.Option;

public class AliPayOptions : IConfigurableOptions
{
    public string AppId { get; set; }
    public string PublicKeyPath { get; set; }
    public string PrivateKeyPath { get; set; }
    public string AliPublicKeyPath { get; set; }
    public string PayNotifyUrl { get; set; }
}