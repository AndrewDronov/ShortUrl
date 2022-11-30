using Microsoft.Extensions.Options;
using QRCoder;
using ShortUrl.Services.Interfaces;
using ShortUrl.Settings;
using System.IO;
using System.Threading.Tasks;


namespace ShortUrl.Services.Implementations
{
    public class QrCodeService : IQrCodeService
    {
        private readonly QrCodeOptions _options;

        public QrCodeService(IOptions<QrCodeOptions> options)
        {
            _options = options.Value;
        }

        public void CreateAndSave(string name, string shortLink)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(shortLink, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);

            if (!Directory.Exists(_options.Path))
            {
                Directory.CreateDirectory(_options.Path);
            }

            var image = qrCode.GetGraphic(20);

            var path = Path.Combine(_options.Path, name + _options.Extension);

            image.Save(path);
        }

        public async Task<byte[]> GetQrCodeByTokenAsync(string token)
        {
            var path = Path.Combine(_options.Path, token + _options.Extension);

            if (!File.Exists(path))
            {
                return null;
            }

            return await File.ReadAllBytesAsync(path);
        }
    }
}