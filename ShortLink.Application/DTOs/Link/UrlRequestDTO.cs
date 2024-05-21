using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShortLink.Application.DTOs.Link
{
    public class UrlRequestDTO
    {
        [Display(Name = "url اصلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? OrginalUrl { get; set; }
    }

    public enum UrlRequestResult
    {
        Success,
        Error
    }
}
