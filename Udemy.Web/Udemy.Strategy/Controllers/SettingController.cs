using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Udemy.Strategy.Enums;
using Udemy.Strategy.Models;

namespace Udemy.Strategy.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            Settings settings = new();
            var claim = User.Claims.FirstOrDefault(x => x.Type == Settings.ClaimDatabaseType);
            if (claim != null)
            {
                settings.DatabaseType = (DatabaseType)Int32.Parse(claim.Value);
            }
            else
            {
                settings.DatabaseType = settings.GetDefaultDatabaseType;
            }
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var newClaim = new Claim(Settings.ClaimDatabaseType, databaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(appUser);

            var claim = claims.FirstOrDefault(x => x.Type == Settings.ClaimDatabaseType);

            if (claim != null)
            {
                await _userManager.ReplaceClaimAsync(appUser, claim, newClaim);
            }
            else
            {
                await _userManager.AddClaimAsync(appUser, newClaim);
            }

            await _signInManager.SignOutAsync();

            var authenticateResult = await HttpContext.AuthenticateAsync();

            await _signInManager.SignInAsync(appUser, authenticateResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}
