using System.ComponentModel.DataAnnotations;

namespace ShortLink.Application.DTOs.Account
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "Mobile Number")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
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

        [Required(ErrorMessage = "{0} is Required")]
        [Display(Name = "User Password")]
        [MaxLength(200, ErrorMessage = "{0} dont max is 200 charactered")]
        [Compare("Password",ErrorMessage ="Password is Not Matched")]
        public string? RePassword { get; set; }
    }

    // enum
    public enum RegisterUserResult
    {
        Success,
        IsMobileExist
    }
}
