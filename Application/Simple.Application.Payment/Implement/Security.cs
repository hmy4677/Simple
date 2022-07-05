using Aop.Api.Util;
using Furion.FriendlyException;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Simple.Application.Payment.Model;
using Simple.Application.Payment.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Application.Payment.Implement;

public class Security
{
    private readonly WechatPayOptions _wechatPayOptions;
    private readonly AliPayOptions _aliPayOptions;

    public Security(IOptions<WechatPayOptions> wechatPayOptions, IOptions<AliPayOptions> aliPayOptions)
    {
        _wechatPayOptions = wechatPayOptions.Value;
        _aliPayOptions = aliPayOptions.Value;
    }

    /// <summary>
    /// 构建微信支付后端请求Authorization
    /// </summary>
    /// <param name="security"></param>
    /// <returns></returns>
    public string BuildAuthorization(SecurityInfo security)
    {
        var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        string nonce = Path.GetRandomFileName();
        var signStr = $"{security.method}\n{security.url}\n{timestamp}\n{nonce}\n{security.bodyjson ?? ""}\n";
        var signature = SHA256WithRSASign(signStr);

        var authorization = $" WECHATPAY2-SHA256-RSA2048 mchid=\"{_wechatPayOptions.Mchid}\",nonce_str=\"{nonce}\",signature=\"{signature}\",timestamp=\"{timestamp}\",serial_no=\"{_wechatPayOptions.CertSerialno}\"";
        return authorization;
    }

    /// <summary>
    /// 小程序支付签名
    /// </summary>
    /// <param name="prepayid"></param>
    /// <returns></returns>
    public WeAppSignInfo MiniAppSign(string prepayid)
    {
        var appId = _wechatPayOptions.Appid;
        var timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var nonceStr = Path.GetRandomFileName();
        var package = $"prepay_id={prepayid}";
        var signStr = $"{appId}\n{timeStamp}\n{nonceStr}\n{package}\n";
        var paySign = SHA256WithRSASign(signStr);
        return new WeAppSignInfo
        {
            appId = appId,
            nonceStr = nonceStr,
            package = package,
            paySign = paySign,
            timeStamp = (int)timeStamp,
            prepay_id = prepayid
        };
    }

    /// <summary>
    /// SHA256 With RSA 签名
    /// </summary>
    /// <param name="signStr">被签串</param>
    /// <returns>签名</returns>
    private protected string SHA256WithRSASign(string signStr)
    {
        if (!File.Exists(_wechatPayOptions.PrivateKeyPath))
        {
            throw Oops.Oh("微信私钥文件不存在");
        }
        using var fileStream = new FileStream(_wechatPayOptions.PrivateKeyPath, FileMode.Open);
        var reader = new StreamReader(fileStream);
        var privateKey = reader.ReadToEnd()
            .Replace("-----BEGIN PRIVATE KEY-----", string.Empty)
            .Replace("-----END PRIVATE KEY-----", string.Empty)
            .Trim();

        var keyData = Convert.FromBase64String(privateKey);
        var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(keyData, out _);
        var signStrdata = Encoding.UTF8.GetBytes(signStr);
        //对 signStr 用 私钥 进行 SHA256 with RSA签名
        //再转base64
        var signature = Convert.ToBase64String(rsa.SignData(signStrdata, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        return signature;
    }


    /// <summary>
    /// 微信解密
    /// </summary>
    /// <param name="associatedData"></param>
    /// <param name="nonce"></param>
    /// <param name="ciphertext"></param>
    /// <returns></returns>
    public string AesGcmDecrypt(string associatedData, string nonce, string ciphertext)
    {
        var AES_KEY = _wechatPayOptions.Key;

        var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
        var aeadParameters = new AeadParameters(
            new KeyParameter(Encoding.UTF8.GetBytes(AES_KEY)),
            128,
            Encoding.UTF8.GetBytes(nonce),
            Encoding.UTF8.GetBytes(associatedData));
        gcmBlockCipher.Init(false, aeadParameters);

        byte[] data = Convert.FromBase64String(ciphertext);
        byte[] plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
        int length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
        gcmBlockCipher.DoFinal(plaintext, length);
        return Encoding.UTF8.GetString(plaintext);
    }

    /// <summary>
    /// 微信验签
    /// </summary>
    /// <param name="signature"></param>
    /// <param name="stamp"></param>
    /// <param name="nonce"></param>
    /// <param name="bodyjson"></param>
    /// <returns></returns>
    public bool VerifyWechat(string signature, string stamp, string nonce, string? bodyjson)
    {
        if (!File.Exists(_wechatPayOptions.PlatCertPath))
        {
            throw Oops.Oh("微信支付平台证书文件不存在");
        }

        var messageData = Encoding.UTF8.GetBytes($"{stamp}\n{nonce}\n{bodyjson}\n");

        var cert = new X509Certificate(_wechatPayOptions.PlatCertPath);
        //var serialno = cert.GetSerialNumberString();
        var resultsTrue = cert.GetPublicKey();

        var myrsa = RSA.Create();
        myrsa.ImportRSAPublicKey(resultsTrue, out _);

        var signature64 = Convert.FromBase64String(signature);
        var result = myrsa.VerifyData(messageData, signature64, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return result;
    }

    /// <summary>
    /// 支付宝验签
    /// </summary>
    /// <param name="signarr"></param>
    /// <returns></returns>
    public bool VerifyAli(Dictionary<string, string> signarr)
    {
        using var fileStream = File.OpenRead(_aliPayOptions.AliPublicKeyPath);
        var reader = new StreamReader(fileStream);
        var publicKey = reader.ReadToEnd();
        var flag = AlipaySignature.RSACheckV1(signarr, publicKey, "UTF-8", "RSA2", false);
        return flag;
    }
}
