using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WhereToMeet.Repository.Models;

namespace WhereToMeet.Controllers
{
    public class AccountController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string externalProvider)
        {
            if (externalProvider != "Google")
            {
                return new BadRequestResult();
            }
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties(externalProvider, redirectUri);
            return Challenge(properties, externalProvider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            } else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = new AppUser { UserName = email, Email = email };
                var userResult = await userManager.CreateAsync(user);
                if (userResult.Succeeded)
                {
                    var loginResult = await userManager.AddLoginAsync(user, info);
                    if (loginResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction(nameof(Login));
        }
    }
}