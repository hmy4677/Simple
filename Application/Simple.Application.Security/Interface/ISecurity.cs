using System;
using Simple.Application.Security.Enum;

namespace Simple.Application.Security.Interface
{
  public interface ISecurity
  {
    /// <summary>
    /// 数字签名
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="privatekeyPath">私钥文件路径</param>
    /// <param name="type">私钥类型</param>
    /// <returns>签名结果</returns>
    string Sha256WithRSASign(string message, string privatekeyPath, PrivateKeyType type);

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="signString">验签串</param>
    /// <param name="publickeyPath">公钥路径</param>
    /// <param name="type">证书类型</param>
    /// <returns>验签结果</returns>
    bool VerfifySign(string message, string signString, string publickeyPath, PublicKeyType type);

    /// <summary>
    /// RSA加密
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="publickeyPath">公钥路径</param>
    /// <returns>加密结果（密文）</returns>
    string RSAEncrypt(string message, string publickeyPath);

    /// <summary>
    /// RSA解密
    /// </summary>
    /// <param name="password">密文</param>
    /// <param name="privatekeyPath">私钥路径</param>
    /// <returns>解密结果（明文）</returns>
    string RSADecrypt(string password, string privatekeyPath);
  }
}

