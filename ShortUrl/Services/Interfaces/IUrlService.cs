using System.Threading.Tasks;

namespace ShortUrl.Services.Interfaces
{
    public interface IUrlService
    {
        public Task<string> GetUrlByTokenAsync(string token);

        public Task<string> GenerateTokenByUrlAsync(string link);
    }
}