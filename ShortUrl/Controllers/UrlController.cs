using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShortUrl.Models;
using ShortUrl.Services.Interfaces;
using System;

namespace ShortUrl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly IQrCodeService _qrCodeService;

        public UrlController(IUrlService urlService, IQrCodeService qrCodeService)
        {
            _urlService = urlService;
            _qrCodeService = qrCodeService;
        }

        [HttpGet("getUrlByToken/{token}")]
        public async Task<ActionResult<string>> GetUrlByToken(string token)
        {
            var link = await _urlService.GetUrlByTokenAsync(token);

            if (link == null)
            {
                return NotFound();
            }

            return link;
        }

        [HttpPost("generateTokenByUrl")]
        public async Task<ActionResult<string>> GenerateTokenByUrl([FromBody] GenerateTokenRequest model)
        {
            var token = await _urlService.GenerateTokenByUrlAsync(model.Link);

           var uriBuilder = new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port ?? 80, token);
           
           _qrCodeService.CreateAndSave(token, uriBuilder.ToString());

            return token;
        }

        [HttpGet("getQrCodeBytoken/{token}")]
        public async Task<IActionResult> GetQrCodeByToken(string token)
        {
            var qrCode = await _qrCodeService.GetQrCodeByTokenAsync(token);

            if (qrCode == null)
            {
                return NotFound();
            }

            return new FileContentResult(qrCode, "image/png");
        }
    }
}