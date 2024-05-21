using System.ComponentModel.DataAnnotations;

namespace ShortLink.Application.DTOs.Account
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "Mobile Number")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        public string? Mobile { get; set; }

        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "User Password")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public enum LoginUserResult
    {
        Success,
        NotFound,
        NotActive
    }

}
