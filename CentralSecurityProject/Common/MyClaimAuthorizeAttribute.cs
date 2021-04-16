using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace CentralSecurityProject.Common
{
    /// <summary>
    /// کلاس مربوط به پیاده سازی اعلام های مجوز دسترسی
    /// با استفاده از کلیم ها می توان دسترسی های استثناء را به کاربر ورودی سیستم داد
    /// </summary>
    public class MyClaimAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// نوع اعلام دسترسی
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// مقدار اعلام دسترسی
        /// </summary>
        public string ClaimValue { get; set; }

        /// <summary>
        /// این متد زمانی اجرا می شود که
        /// مجاز بودن به مشکل بر می خورد
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Home/Unauthorized"); // ارسال به صفحه مربوط به نداشتن مجوز دسترسی
        }

        /// <summary>
        /// متد مربوط به کنترل دسترسی مجاز کاربر
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated) return false;

            if (!(httpContext.User.Identity is ClaimsIdentity)) return false;

            //return base.AuthorizeCore(httpContext);
            var identity = (ClaimsIdentity)httpContext.User.Identity; // جهت بررسی نام و مقدار اعلام دسترسی
            return identity.HasClaim(m => m.Type == ClaimType && m.Value == ClaimValue); // این مقادیر در بانک اطلاعاتی چک می شود و در صورت عدم ورود به پنجره ورود به سیستم هدایت می کند
        }
    }
}