using Microsoft.AspNetCore.Mvc;
using ShortLink.Application.Interfaces;

namespace ShortLink.Web.Controllers
{
    public class SiteBaseController : Controller
    {
        // notification 
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
