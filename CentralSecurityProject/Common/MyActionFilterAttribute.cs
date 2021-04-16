using System.Linq;
using System.Web.Mvc;
using CentralSecurityProject.Models;

namespace CentralSecurityProject.Common
{
    /// <summary>
    /// تعریف یک خصوصیت به جهت تعریف عمومی در سطح برنامه
    /// </summary>
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cartbotName = "[Unknown]";
            int unreadQty = 0;
            int notcheckQty = 0;
            int unreferralQty = 0;
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                ApplicationDbContext _context = new ApplicationDbContext();
                ApplicationUser aspNetUser = _context.Users.FirstOrDefault(f => f.UserName == filterContext.HttpContext.User.Identity.Name);
                var objUser = _context.UserModels.Where(x => x.AspNetUserId == aspNetUser.Id).FirstOrDefault();
                if(objUser != null) cartbotName = string.Format("[ {0} ]", objUser.UserName);
                unreadQty = _context.RequestModels.Where(x => x.RequestStatusId == 1).Count();
                notcheckQty = _context.RequestModels.Where(x => x.InsertUserId == aspNetUser.Id).Where(x => x.RequestStatusId == 1).Count();
                unreferralQty = _context.RequestModels.Where(x => x.InsertUserId == aspNetUser.Id).Where(x => x.RequestStatusId == 2).Count();
            }

            filterContext.Controller.ViewBag.IsAdmin = filterContext.HttpContext.User.IsInRole("administrator");
            filterContext.Controller.ViewBag.CartbotName = cartbotName; // عنوان کاربر
            filterContext.Controller.ViewBag.UnreadQty = string.Format(" [ {0} ] ", unreadQty.ToString()); // تعداد درخواست های بررسی نشده
            filterContext.Controller.ViewBag.NotCheckQty = string.Format(" [ {0} ] ", notcheckQty.ToString()); // تعداد درخواست های بررسی نشده
            filterContext.Controller.ViewBag.UnReferralQty = string.Format(" [ {0} ] ", unreferralQty.ToString()); // تعداد درخواست های ارجاع نشده

            base.OnActionExecuted(filterContext);
        }
    }
}