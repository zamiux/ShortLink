using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.DTOs.Account;
using ShortLink.Application.Interfaces;
using ShortLink.Domain.Models.Account;
using System.Security.Claims;

namespace ShortLink.Web.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Cosntructor
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region User Register

        [HttpGet("register")]
        public async Task<IActionResult> RegisterUser()
        {
            return View();
        }


        [HttpPost("register"),ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if (ModelState.IsValid) {
                var result = await _userService.RegisterUser(register);
                switch (result)
                {
                    case RegisterUserResult.Success:
                        TempData[SuccessMessage] = "User Created... !!!";
                        return Redirect("/");

                    case RegisterUserResult.IsMobileExist:
                        TempData[ErrorMessage] = "User Not Valid.. !!!";
                        ModelState.AddModelError("Mobile","Mobile Phone is Duplicated. !! ");
                        break;
                }
            }
            return View(register);
        }
        #endregion

        #region User Login
        [HttpGet("login")]
        public async Task<IActionResult> LoginUser()
        {
            return View();
        }


        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUser(login);
                switch (result)
                {
                    case LoginUserResult.Success:
                       
                        #region authentication info
                        var user = await _userService.GetUserByMobile(login.Mobile);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principle = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };
                        await HttpContext.SignInAsync(principle, properties);

                        #endregion

                        TempData[SuccessMessage] = "You are Login Now";
                        return Redirect("/");

                    case LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "User Not Found!! .. ";
                        break;
                    case LoginUserResult.NotActive:
                        TempData[WarningMessage] = "This User is Not Activated!! .. ";
                        break;
                }
            }
            return View(login);
        }
        #endregion

        #region user logout
        [HttpGet("log-Out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            TempData[SuccessMessage] = "Your Account LogOut Now";
            return Redirect("/");
        }
        #endregion
        
    }
}
