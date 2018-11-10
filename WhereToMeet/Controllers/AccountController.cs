using Microsoft.AspNetCore.Mvc;

namespace WhereToMeet.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}