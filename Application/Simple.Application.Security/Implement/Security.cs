using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Simple.Application.Security.Enum;
using Simple.Application.Security.Interface;

namespace Simple.Application.Security.Implement
{
  public class Security : ISecurity
  {

    /// <summary>
    /// 数字签名
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="privatekeyPath">私钥文件路径</param>
    /// <param name="type">私钥类型</param>
    /// <returns>签名结果</returns>
    public string Sha256WithRSASign(string message,string privatekeyPath,PrivateKeyType type)
    {
      var privatekey64 = GetKeyData(privatekeyPath);
      var rsa = RSA.Create();
      if (type == PrivateKeyType.Pkcs8)
      {
        rsa.ImportPkcs8PrivateKey(privatekey64, out _);
      }
      else
      {
        rsa.ImportRSAPrivateKey(privatekey64, out _);
      }
      var messageData = Encoding.UTF8.GetBytes(message);
      var signature = rsa.SignData(messageData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
      var result = Convert.ToBase64String(signature);
      return result;
    }

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="signString">验签串</param>
    /// <param name="publickeyPath">公钥路径</param>
    /// <param name="type">证书类型</param>
    /// <returns>验签结果</returns>
    public bool VerfifySign(string message,string signString,string publickeyPath, PublicKeyType type)
    {
      var rsa = RSA.Create();
      var publickey64 = GetKeyData(publickeyPath);
      if (type == PublicKeyType.SubjectInfo)
      {
        rsa.ImportSubjectPublicKeyInfo(publickey64, out _);
      }
      else if (type == PublicKeyType.RSA)
      {
        rsa.ImportRSAPublicKey(publickey64, out _);
      }
      else
      {
        var cert = new X509Certificate(publickeyPath);
        var publickeyData = cert.GetPublicKey();
        rsa.ImportRSAPublicKey(publickeyData, out _);
      }

      var messageData = Encoding.UTF8.GetBytes(message);
      var sign64 = Convert.FromBase64String(signString);
      var result = rsa.VerifyData(messageData, sign64, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
      return result;
    }

    /// <summary>
    /// RSA加密
    /// </summary>
    /// <param name="message">明文</param>
    /// <param name="publickeyPath">公钥路径</param>
    /// <returns>加密结果（密文）</returns>
    public string RSAEncrypt(string message,string publickeyPath)
    {
      var rsa = RSA.Create();
      var keyData = GetKeyData(publickeyPath);
      rsa.ImportSubjectPublicKeyInfo(keyData, out _);
      var messageData = Encoding.UTF8.GetBytes(message);
      var encrypt = rsa.Encrypt(messageData, RSAEncryptionPadding.Pkcs1);
      var encrypt64String = Convert.ToBase64String(encrypt);
      return encrypt64String;
    }

    /// <summary>
    /// RSA解密
    /// </summary>
    /// <param name="password">密文</param>
    /// <param name="privatekeyPath">私钥路径</param>
    /// <returns>解密结果（明文）</returns>
    public string RSADecrypt(string password,string privatekeyPath)
    {
      var rsa = RSA.Create();
      var keyData = GetKeyData(privatekeyPath);
      rsa.ImportPkcs8PrivateKey(keyData, out _);

      var password64 = Convert.FromBase64String(password);
      var decrypt = rsa.Decrypt(password64, RSAEncryptionPadding.Pkcs1);
      var decryptString = Encoding.UTF8.GetString(decrypt);
      return decryptString;
    }





    private static bool IsExist(string filePath)
    {
      var isexist = File.Exists(filePath);
      if (isexist)
      {
        return true;
      }
      else
      {
        throw new Exception("PublicKey file isn't exist");
      }
    }

    private static byte[] GetKeyData(string filePath)
    {
      IsExist(filePath);
      using var fileStream = new FileStream(filePath, FileMode.Open);
      var keyStr = new StreamReader(filePath).ReadToEnd();
      var key64 = Convert.FromBase64String(filePath);
      return key64;
    }
  }
}

