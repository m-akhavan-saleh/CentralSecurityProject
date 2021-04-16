using System;
using System.Web.Mvc;

namespace CentralSecurityProject.Common
{
    /// <summary>
    /// تنظیم کردن خصوصیت مربوط به مجاز بودن دسترسی ها
    /// مطابق تنظیمات خاص در نظر گرفته شده در برنامه
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
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
        /// این متد به جهت کنترل
        /// مجاز بودن دسترسی ها فراخوانی می شود
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (base.AuthorizeCore(filterContext.HttpContext))
            {
                // مجاز بودن دسترسی
                base.OnAuthorization(filterContext);
            }
            else
            {
                // مجاز نبودن دسترسی
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}