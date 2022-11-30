using Microsoft.AspNetCore.Mvc;
using ShortUrl.Services.Interfaces;
using System.Threading.Tasks;

namespace ShortUrl.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IUrlService _urlService;

        public RedirectController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<IActionResult> Redirect()
        {
            var token = HttpContext.Request.Path.Value?.TrimStart('/');

            var link = await _urlService.GetUrlByTokenAsync(token);

            if (link == null)
            {
                return NotFound();
            }

            return Redirect(link);
        }
    }
}