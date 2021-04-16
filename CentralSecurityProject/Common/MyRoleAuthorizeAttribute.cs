using System.Web;
using System.Web.Mvc;

namespace CentralSecurityProject.Common
{
    /// <summary>
    /// کلاس خصوصیت بررسی نقش کاربر
    /// وارد شده به سیستم
    /// </summary>
    public class MyRoleAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// خصوصیت مربوط به تعریف
        /// نقش کاربر ورودی به سیستم
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// این متد زمانی اجرا می شود که
        /// مجاز بودن به مشکل بر می خورد
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectResult("~/Home/RoleUnauthorized"); // ارسال به صفحه مربوط به نداشتن مجوز دسترسی

            //if (filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //    filterContext.Result = new HttpStatusCodeResult(403, "Sorry, you do not have the required permission to perform this action.");

            //}
            //else
            //{
            //    var viewResult = new ViewResult();

            //    viewResult.ViewName = "~/Views/Errors/_Unauthorized.cshtml";
            //    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            //    filterContext.HttpContext.Response.StatusCode = 403;
            //    filterContext.Result = viewResult;
            //}
        }

        /// <summary>
        /// متد مربوط به کنترل نقش کاربر
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated) return false;

            //return base.AuthorizeCore(httpContext);
            return httpContext.User.IsInRole(RoleName); // این مقادیر در بانک اطلاعاتی چک می شود و در صورت عدم ورود به پنجره ورود به سیستم هدایت می کند
        }
    }
}