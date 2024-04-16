using weblog.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using weblog.CoreLayer.DTOs.User;
using weblog.CoreLayer.Services.User;

namespace Myblog.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {

        #region properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} راوارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} راوارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} راوارد کنید")]
        [MinLength(6, ErrorMessage = "{0}باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }
        #endregion



        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = _userService.RegisterUser(new UserRegisterDto()
            {
                FullName = FullName,
                UserName = UserName,
                Password = Password
            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
