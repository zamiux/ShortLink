using ShortLink.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace ShortLink.Domain.Models.Account
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage ="{0} is Required")]
        [Display(Name = "Mobile Number")]
        [MaxLength(200,ErrorMessage = "{0} dont max is 200 charactered")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "First Name")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "Last Name")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "User Password")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        public string? Password { get; set; }

        public string? MobileActiveCode { get; set; }

        [Display(Name = "Mobile is Active / Non Active")]
        public bool IsMobileActive { get; set; } = false;

        [Display(Name = "User is Block / Non Block")]
        public bool IsUserBlock { get; set; } = false;

        [Display(Name = "User is Admin / Non Admin")]
        public bool IsAdmin { get; set; } = false;

    }
}
