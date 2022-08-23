using Simple.Application.Payment.Model;

namespace Simple.Application.Payment.Interface;

/// <summary>
/// 支付安全接口
/// </summary>
public interface ISecurity
{
    /// <summary>
    /// 构建微信支付后端请求Authorization
    /// </summary>
    /// <param name="security"></param>
    /// <returns></returns>
    string BuildAuthorization(SecurityInfo security);

    /// <summary>
    /// 小程序支付签名
    /// </summary>
    /// <param name="prepayid"></param>
    /// <returns></returns>
    WeAppSignInfo MiniAppSign(string prepayid);

    /// <summary>
    /// 微信解密
    /// </summary>
    /// <param name="associatedData"></param>
    /// <param name="nonce"></param>
    /// <param name="ciphertext"></param>
    /// <returns></returns>
    string AesGcmDecrypt(string associatedData, string nonce, string ciphertext);

    /// <summary>
    /// 微信验签
    /// </summary>
    /// <param name="signature"></param>
    /// <param name="stamp"></param>
    /// <param name="nonce"></param>
    /// <param name="bodyjson"></param>
    /// <returns></returns>
    bool VerifyWechat(string signature, string stamp, string nonce, string bodyjson);

    /// <summary>
    /// 支付宝验签
    /// </summary>
    /// <param name="signarr"></param>
    /// <returns></returns>
    bool VerifyAli(Dictionary<string, string> signarr);
}