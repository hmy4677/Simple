using System;
using Simple.Application.QrCode.Enum;

namespace Simple.Application.QrCode.Interface
{
  public interface IQrCode
  {
    dynamic MakeQrCode(string content, MakeQrType type, string path, int width = 512);
  }
}

