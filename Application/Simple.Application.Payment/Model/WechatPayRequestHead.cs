using Newtonsoft.Json;

namespace Simple.Application.Payment.Model;

public class WechatPayRequestHead
{
    [JsonProperty("Content-Type")]
    public string ContentType { get; set; } = "application/json";

    [JsonProperty("Accept")]
    public string Accept { get; set; } = "application/json";

    public string Authorization { get; set; }
}