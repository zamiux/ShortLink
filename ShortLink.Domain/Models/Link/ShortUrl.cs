using ShortLink.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models.Link
{
    public class ShortUrl : BaseEntity
    {
        #region properties
        [Display(Name = "url اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public Uri? OrginalUrl { get; set; }

        [Display(Name = "url کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(400, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public Uri? Value { get; set; }

        [Display(Name = "نوکن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(40, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Token { get; set; }
        #endregion

        #region relations
        public ICollection<RequestUrl>? RequestUrls { get; set; }
        #endregion
    }
}
