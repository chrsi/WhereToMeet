using Microsoft.AspNetCore.Identity;
using Identity = Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult ExternalLogin(string externalProvider, string returnUrl = null)
        {
            if (externalProvider != "Google")
            {
                return new BadRequestResult();
            }
            var redirectUri = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(externalProvider, redirectUri);
            return Challenge(properties, externalProvider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            bool loginSuccessful = await LoginOrCreateExternalUser();
            if (loginSuccessful)
            {
                if (returnUrl == null) returnUrl = Url.Action("Index", "Home");
                return LocalRedirect(returnUrl);
            } else
            {
                return RedirectToAction(nameof(Login));
            }
        }

        /// <summary>
        /// Tries to login a user from an external Provider. If the used does not exist, it will be created.
        /// </summary>
        /// <returns>A Flag indicating whether the user logged in successfully.</returns>
        private async Task<bool> LoginOrCreateExternalUser()
        {
            ExternalLoginInfo loginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return false;
            }

            Identity.SignInResult loginResult = await signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (loginResult.Succeeded)
            {
                return true;
            }

            AppUser newUser = CreateAppUser(loginInfo);
            IdentityResult identityResult = await userManager.CreateAsync(newUser);
            if (!identityResult.Succeeded)
            {
                return false;
            }

            await signInManager.SignInAsync(newUser, isPersistent: false);
            return true;
        }

        /// <summary>
        /// Creates an app used based on external login info.
        /// </summary>
        /// <param name="loginInfo">Info about the user provided by an external provider.</param>
        /// <returns>Instance of a local AppUser</returns>
        private AppUser CreateAppUser(ExternalLoginInfo loginInfo)
        {
            var email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var profileImage = loginInfo.Principal.FindFirstValue("profileImg");
            return new AppUser { UserName = email, Email = email };
        }
    }
}