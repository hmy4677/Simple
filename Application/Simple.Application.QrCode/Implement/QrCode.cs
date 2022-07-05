using Furion.DependencyInjection;
using Furion.FriendlyException;
using Simple.Application.QrCode.Enum;
using Simple.Application.QrCode.Interface;
using SkiaSharp;
using SkiaSharp.QrCode;

namespace Simple.Application.QrCode.Implement;

public class QrCode : IQrCode, ITransient
{
    public dynamic MakeQrCode(string content, MakeQrType type, string path, int width = 512)
    {
        using var generator = new QRCodeGenerator();
        var qr = generator.CreateQrCode(content, ECCLevel.Q);
        var info = new SKImageInfo(width, width);
        using var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;
        canvas.Render(qr, info.Width, info.Height);

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);

        if (type == MakeQrType.ToByteArray)
        {
            return data.ToArray();
        }
        if (string.IsNullOrEmpty(path))
        {
            throw Oops.Oh("No file path");
        }
        using var stream = File.OpenWrite(@$"{path}");
        data.SaveTo(stream);
        return true;
    }
}