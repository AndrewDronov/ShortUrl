using System.Threading.Tasks;

namespace ShortUrl.Services.Interfaces
{
    public interface IQrCodeService
    {
        public void CreateAndSave(string name, string shortLink);

        public Task<byte[]> GetQrCodeByTokenAsync(string token);
    }
}