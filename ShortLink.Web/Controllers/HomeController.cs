using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.DTOs.Link;
using ShortLink.Application.Interfaces;
using ShortLink.Web.Models;
using System.Diagnostics;

namespace ShortLink.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly ILogger<HomeController> _logger;

        #region constractor
        private readonly ILinkService _linkService;
        public HomeController(ILinkService linkService, ILogger<HomeController> logger)
        {
            _linkService = linkService;

            _logger = logger;
        }
        #endregion


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UrlRequestDTO urlRequest)
        {
            if(ModelState.IsValid)
            {
                if (urlRequest.OrginalUrl.Contains("https://") || urlRequest.OrginalUrl.Contains("http://"))
                {
                    var url = new Uri(urlRequest.OrginalUrl);
                    var shortUrl = _linkService.QuickShortUrl(url);

                    var result = await _linkService.AddLink(shortUrl);
                    switch (result)
                    {
                        case UrlRequestResult.Error:
                            TempData[ErrorMessage] = "مشکلی رخ داده است";
                            break;
                        case UrlRequestResult.Success:
                            TempData[SuccessMessage] = "لینک شما با موفقیت کوتاه شد";
                            ViewBag.isSuccess = true;
                            ViewBag.UserShortLink = shortUrl.Value.ToString();
                            break;
                    }

                }
                else
                {
                    TempData[ErrorMessage] = "لطفا لینک خود را با https یا http وارد نمایید";
                    return View(urlRequest);
                }
            }
            return View(urlRequest);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}