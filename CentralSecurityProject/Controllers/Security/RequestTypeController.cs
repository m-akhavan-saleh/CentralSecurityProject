using CentralSecurityProject.Common;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به نوع درخواست
    /// </summary>
    //[Authorize(Roles = "administrator")] // به واسطه این خصوصیت تمام متدهای داخل این کنترلر نیاز دارد که کاربر آن وارد سیستم شده باشد
    [MyAuthorize(Roles = "administrator")] // به واسطه این خصوصیت تمام متدهای داخل این کنترلر نیاز دارد که کاربر آن وارد سیستم شده باشد
    public class RequestTypeController : BaseController<Models.Security.RequestTypeModel>
    {
        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        public RequestTypeController()
        {
            MyInitialize();
        }

        /// <summary>
        /// متد مربوط به تنظیمات کنترلر نوع درخواست
        /// </summary>
        private void MyInitialize()
        {
            ViewBag.Title = "نوع درخواست";
        }
    }
}