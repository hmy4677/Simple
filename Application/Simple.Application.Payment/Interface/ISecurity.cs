using Simple.Application.Payment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.Payment.Interface;

public interface ISecurity
{
    string BuildAuthorization(SecurityInfo security);
    WeAppSignInfo MiniAppSign(string prepayid);
    string AesGcmDecrypt(string associatedData, string nonce, string ciphertext);
    bool VerifyWechat(string signature, string stamp, string nonce, string? bodyjson);
    bool VerifyAli(Dictionary<string, string> signarr);
}
