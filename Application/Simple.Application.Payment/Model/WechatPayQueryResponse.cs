namespace Simple.Application.Payment.Model;

public class WechatPayQueryResponse
{
    public string appid { get; set; }
    public string mchid { get; set; }
    public string out_trade_no { get; set; }
    public string transaction_id { get; set; }
    public string trade_type { get; set; }
    public string trae_state { get; set; }
    public string trade_state_desc { get; set; }
    public string bank_type { get; set; }
    public string success_time { get; set; }
    public PayerInfo payer { get; set; }
    public AmountInfo amount { get; set; }
}