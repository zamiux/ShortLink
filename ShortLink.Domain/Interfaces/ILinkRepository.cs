using ShortLink.Domain.Models.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLink.Domain.Interfaces
{
    public interface ILinkRepository: IAsyncDisposable
    {
        #region link
        Task AddLink(ShortUrl url);
        Task AddOs(Os os);
        Task AddDevive(Device device);
        Task AddBrower(Brower brower);
        Task<ShortUrl> FindUrlByToken(string token);

        Task AddRequestURL(RequestUrl url);
        #endregion
        Task SaveChange();
    }
}
