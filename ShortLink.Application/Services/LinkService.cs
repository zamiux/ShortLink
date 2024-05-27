using ShortLink.Application.DTOs.Link;
using ShortLink.Application.Generator;
using ShortLink.Application.Interfaces;
using ShortLink.Domain.Interfaces;
using ShortLink.Domain.Models.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace ShortLink.Application.Services
{
    public class LinkService : ILinkService
    {
        #region constractor
        private readonly ILinkRepository _linkRepository;
        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }


        #endregion

        #region link
        public ShortUrl QuickShortUrl(Uri uri)
        {
            var shortUrl = new ShortUrl();
            shortUrl.OrginalUrl = uri;
            shortUrl.CreateDate = DateTime.Now;
            shortUrl.Token = Generate.Token();
            shortUrl.Value = new Uri($"http://localhost:5296/{shortUrl.Token}");
            return shortUrl;
        }
        public async Task<UrlRequestResult> AddLink(ShortUrl url)
        {
            if (url == null) return UrlRequestResult.Error;

            await _linkRepository.AddLink(url);
            await _linkRepository.SaveChange();

            return UrlRequestResult.Success;
        }

        public async Task AddUserAgent(string userAgnet)
        {
            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(userAgnet);

            var Os = new Os()
            {
                Family = client.OS.Family,
                Major = client.OS.Major,
                Minor = "No Data",
                CreateDate = DateTime.Now
            };
            await _linkRepository.AddOs(Os);


            var device = new ShortLink.Domain.Models.Link.Device
            {
                IsBot = client.Device.IsSpider,
                Brand = client.Device.Brand,
                Family = client.Device.Family,
                Model = client.Device.Model,
                CreateDate = DateTime.Now
            };
            await _linkRepository.AddDevive(device);

            var brower = new ShortLink.Domain.Models.Link.Brower
            {
                Family = client.UA.Family,
                Major = client.UA.Major,
                Minor = client.UA.Minor,
                CreateDate = DateTime.Now
            };
            await _linkRepository.AddBrower(brower);
            await _linkRepository.SaveChange();
        }

        public async Task<ShortUrl> FindUrlByToken(string token)
        {
            return await _linkRepository.FindUrlByToken(token);
        }

        public async Task AddRequestUrl(string token)
        {
            var short_url = _linkRepository.FindUrlByToken(token);
            var new_request_url = new RequestUrl()
            {
                CreateDate = DateTime.Now,
                ShortUrlId = short_url.Id,
                RequestDataTime = DateTime.Now,
                IsDelete = false

            };
            
            await _linkRepository.AddRequestURL(new_request_url);
            await _linkRepository.SaveChange();

        }
        #endregion
    }
}
