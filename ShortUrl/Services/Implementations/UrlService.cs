using ShortUrl.Models;
using ShortUrl.Services.Interfaces;
using System.Threading.Tasks;

namespace ShortUrl.Services.Implementations
{
    public class UrlService : IUrlService
    {
        private readonly ShortUrlContext _context;
        private readonly IQrCodeService _qrCodeService;

        public UrlService(ShortUrlContext context, IQrCodeService qrCodeService)
        {
            _context = context;
            _qrCodeService = qrCodeService;
        }

        public async Task<string> GetUrlByTokenAsync(string token)
        {
            var url = await _context.Urls.FindAsync(token);

            return url?.Link;
        }

        public async Task<string> GenerateTokenByUrlAsync(string link)
        {
            var url = new Url { Link = link };

            _context.Urls.Add(url);

            await _context.SaveChangesAsync();
            
            return url.Token;
        }
    }
}