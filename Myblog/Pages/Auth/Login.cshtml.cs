using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using weblog.CoreLayer.DTOs.User;
using weblog.CoreLayer.Services.User;
using weblog.CoreLayer.Utilities;

namespace Myblog.Pages.Auth
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {

        [Required(ErrorMessage = "نام کاربری را وارد کنید ")]
        [BindProperty]
        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        [BindProperty]
        public string Password { get; set; }

        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var user = _userService.LoginUser(new LoginUserDto()
            {
                UserName = UserName,
                Password = Password
            });
            if (user == null)
            {
                ModelState.AddModelError("UserName", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }
            #region Auth
            List<Claim> claims = new List<Claim>()
            {
                new("TEST","TEST"),
                new(ClaimTypes.NameIdentifier,user.userId.ToString()),
                new(ClaimTypes.Name,user.FullName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            #endregion
            return RedirectToPage("../Index");
        }
    }
}
