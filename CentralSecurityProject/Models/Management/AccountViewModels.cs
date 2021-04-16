using CentralSecurityProject.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CentralSecurityProject.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    /// <summary>
    /// کلاس مربوط به ورود کاربر به سیستم
    /// </summary>
    public class LoginViewModel
    {
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Username", Order = 0, Description = "کاربر")]
        public string Username { get; set; }

        /*
        [MyRequired]
        //[Display(Name = "Email")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Email", Order = 1, Description = "صندوق پستی الکترونیکی")]
        [EmailAddress]
        public string Email { get; set; }
        */

        [MyRequired]
        [DataType(DataType.Password)] 
        //[Display(Name = "Password")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Password", Order = 2, Description = "کلمه عبور")]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RememberMe", Order = 3, Description = "مرا بخاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// کلاس مربوط به مدل عضویت در سایت
    /// یا به عبارتی همان برنامه تحت وب
    /// </summary>
    public class RegisterViewModel
    {
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Username", Order = 0, Description = "نام کاربری")]
        public string Username { get; set; }

        [MyRequired]
        [EmailAddress]
        //[Display(Name = "Email")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Email", Order = 0, Description = "صندوق پستی الکترونیکی")]
        public string Email { get; set; }

        [MyRequired]
        [MyStringLength(100, MinimumLength = 3)]
        [DataType(DataType.Password)]
        //[Display(Name = "Password")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Password", Order = 0, Description = "کلمه عبور")]
        public string Password { get; set; }

        [DataType(DataType.Password)] // در زمان نمایش در داخل کنترلر بصورت کراکتر ستاره دار نمایش داده می شود
        //[Display(Name = "Confirm password")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ConfirmPassword", Order = 0, Description = "تکرار کلمه عبور")]
        [MyCompare("Password")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// کلاس مربوط به تنظیم مجدد کلمه عبور
    /// </summary>
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    /// <summary>
    /// کلاس مربوط به فراموش کردن کلمه عبور
    /// </summary>
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
