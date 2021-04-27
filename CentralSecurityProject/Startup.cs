using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using CentralSecurityProject.Models;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(CentralSecurityProject.Startup))] // Define Startup Class :: تعریف کلاس تنظیمات پروژه تحت وب
namespace CentralSecurityProject
{
    /// <summary>
    /// کلاس تنظیمات پروژه تحت وب
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        /// <param name="app">پارامتر ورودی</param>
        public void Configuration(IAppBuilder app)
        {
            // شروع اجرای تنظیمات برنامه تحت وب : قدم دوم
            ConfigureAuth(app);

            // تنظیمات خاص برنامه تحت وب
            CreateMyWebsiteDefaults();
        }


        /// <summary>
        /// متد مربوط به تنظیم پیش فرض های سایت
        /// </summary>
        private void CreateMyWebsiteDefaults()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                #region Create New Role With RoleManager Class
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists(Common.MyStringEnum.GetStringValue(Common.Role.Administrator))) // تعریف نقش مدیر سیستم
                {
                    IdentityResult result = roleManager.Create(new IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.Administrator)));
                    if (!result.Succeeded) return; // در صورت بروز خطا ادامه برنامه انجام نشود
                }

                if (!roleManager.RoleExists(Common.MyStringEnum.GetStringValue(Common.Role.Support))) // تعریف نقش پشتیبان سیستم
                {
                    roleManager.Create(new IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.Support)));
                }

                if (!roleManager.RoleExists(Common.MyStringEnum.GetStringValue(Common.Role.User))) // تعریف نقش کاربر سیستم
                {
                    roleManager.Create(new IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.User)));
                }

                #endregion

                #region Create New User With ApplicationUserManager Class
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                #region [Define Admin User]
                ApplicationUser adminUser = userManager.Find("admin", "@dmin$tr@tor");
                if (adminUser == null)
                {
                    // Not Exist User
                    adminUser = new ApplicationUser()
                    {
                        UserName = "admin",
                        Email = "admin@centeralsecurity.com"
                    };

                    IdentityResult result = userManager.Create(adminUser, "@dmin$tr@tor"); // تعریف کاربر مدیر سیستم
                    if (result.Succeeded)
                    {
                        // دادن نقش مدیر سیستم به کاربر مدیر سیستم در صورت ایجاد موفقیت آمیز کاربر مدیر سیستم
                        userManager.AddToRole(adminUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.Administrator));
                        userManager.AddClaim(adminUser.Id, new System.Security.Claims.Claim("ApplicationGroup", "Create"));
                    }
                }
                else
                {
                    // Exist User
                    // دادن نقش مدیر سیستم به کاربر مدیر سیستم در صورت وجود کاربر مدیر سیستم
                    userManager.AddToRole(adminUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.Administrator));
                    userManager.AddClaim(adminUser.Id, new System.Security.Claims.Claim("ApplicationGroup", "Create"));
                }

                #endregion

                #region [Define Support User]
                ApplicationUser supportUser = userManager.Find("supportuser", "123");
                if (supportUser == null)
                {
                    // Not Exist User
                    supportUser = new ApplicationUser()
                    {
                        UserName = "supportuser",
                        Email = "supportuser@centeralsecurity.com"
                    };

                    IdentityResult result = userManager.Create(supportUser, "123456"); // تعریف کاربر پشتیبان سیستم
                    if (result.Succeeded)
                    {
                        // دادن نقش کاربر پشتیبان سیستم به کاربر پشتیبان سیستم در صورت ایجاد موفقیت آمیز کاربر پشتیبان سیستم
                        userManager.AddToRole(supportUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.Support));
                    }
                }
                else
                {
                    // Exist User
                    // دادن نقش کاربر پشتیبان سیستم به کاربر پشتیبان سیستم در صورت وجود کاربر پشتیبان سیستم
                    userManager.AddToRole(supportUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.Support));
                }

                #endregion

                #region [Define Normal User]
                ApplicationUser endUser = userManager.Find("enduser", "123456");
                if (endUser == null)
                {
                    // Not Exist User
                    endUser = new ApplicationUser()
                    {
                        UserName = "enduser",
                        Email = "enduser@centeralsecurity.com"
                    };

                    IdentityResult result = userManager.Create(endUser, "123456"); // تعریف کاربر عادی سیستم
                    if (result.Succeeded)
                    {
                        // دادن نقش کاربر سیستم به کاربر عادی سیستم در صورت ایجاد موفقیت آمیز کاربر عادی سیستم
                        userManager.AddToRole(endUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.User));
                    }
                }
                else
                {
                    // Exist User
                    // دادن نقش کاربر عادی سیستم به کاربر عادی سیستم در صورت وجود کاربر عادی سیستم
                    userManager.AddToRole(endUser.Id, Common.MyStringEnum.GetStringValue(Common.Role.User));
                }

                #endregion

                #endregion
            }
        }
    }
}
