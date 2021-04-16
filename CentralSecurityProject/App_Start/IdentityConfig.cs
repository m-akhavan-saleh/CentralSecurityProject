using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using CentralSecurityProject.Models;

namespace CentralSecurityProject
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// کلاس مربوط به تنظیمات پروفایل کاربر سیستم
    /// </summary>
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames :: تنظیمات مربوط به کلمه کاربری
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false, // تنظیم مربوط به اینکه به کاربر تنها اجازه بدهد که کلمه کاربری را بصورت کراکتری تعریف نماید
                RequireUniqueEmail = true // اجازه ثبت کاربر با ایمیل تکراری را کنترل می کند
            };

            // Configure validation logic for passwords :: تنظیمات مربوط به کلمه عبور
            manager.PasswordValidator = new PasswordValidator // PasswordValidator :: این امکان وجود دارد تا بتوان از این کلاس ارث بری کرد تا بتوان هم خصوصیت جدید و هم ترجمه خطا ها را نیز انجام داد
            {
                RequiredLength = 3, // Default : 6
                RequireNonLetterOrDigit = false, // Default : true
                RequireDigit = false, // Default : ture
                RequireLowercase = false, // Default : ture
                RequireUppercase = false, // Default : ture
            };

            // Configure user lockout defaults :: تنظیمات مربوط به قفل شدن کاربر سیستم در صورت ورود اشتباه کلمه عبور
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here. :: تنظیمات مربوط به سرویس ارسال ایمیل و سرویس ارسال پیامک
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider; // استفاده از پرووایدر ها جهت هش کردن کلمه عبور
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        /// <summary>
        /// کلاس ارث برده شده جهت اضافه کردن
        /// متدهای کنترلی اضافه به سیستم
        /// </summary>
        internal class MyPasswordValidator : PasswordValidator
        {
            public MyPasswordValidator()
            {
            }

            public override Task<IdentityResult> ValidateAsync(string item)
            {
                return base.ValidateAsync(item);
            }
        }
    }

    /// <summary>
    /// کلاس مربوط به تنظیمات ورود/خروج کاربر سیستم
    /// </summary>
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
