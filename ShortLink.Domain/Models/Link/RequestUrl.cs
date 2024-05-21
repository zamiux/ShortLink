using ShortLink.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortLink.Domain.Models.Link
{
    public class RequestUrl : BaseEntity
    {
        #region properties
        [Display(Name = "کد url")]
        public long ShortUrlId { get; set; }

        [Display(Name = "زمان درخواست")]
        public DateTime RequestDataTime { get; set; }
        #endregion

        #region relations
        public ShortUrl ShortUrl { get; set; }
        #endregion
    }
}
