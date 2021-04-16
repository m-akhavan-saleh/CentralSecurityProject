using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به تعریف کاربر برنامه
    /// </summary>
    public class AppUserController : BaseController<Models.Security.AppUserModel>
    {
        private int _applicationId = 0;
        private int _userId = 0;

        protected override void Initialize(RequestContext requestContext)
        {
            ViewBag.Title = "کاربر برنامه/زیر سیستم";

            if (!string.IsNullOrEmpty(requestContext.HttpContext.Request.QueryString["ApplicationId"]))
            {
                int.TryParse(requestContext.HttpContext.Request.QueryString["ApplicationId"], out _applicationId);
            }
            ViewBag.ApplicationId = _applicationId;
            ViewBag.Applications = new SelectList(_context.ApplicationModels.Where(x => x.IsActive).
                Where(x => x.ApplicationId == _applicationId || _applicationId == 0), "ApplicationId", "ApplicationName");

            if (!string.IsNullOrEmpty(requestContext.HttpContext.Request.QueryString["UserId"]))
            {
                int.TryParse(requestContext.HttpContext.Request.QueryString["UserId"], out _userId);
            }
            ViewBag.UserId = _userId;
            ViewBag.Users = new SelectList(_context.UserModels.Where(x => x.IsActive).
                Where(x => x.UserId == _userId || _userId == 0), "UserId", "DisplayUserName");

            base.Initialize(requestContext);
        }

        /// <summary>
        /// تعریف مسیر کنترلر مورد نظر
        /// </summary>
        /// <returns>QueryString</returns>
        protected override object CreateRoutValues()
        {
            if (_applicationId != 0)
            {
                return new { ApplicationId = _applicationId };
            }
            else if (_userId != 0)
            {
                return new { UserId = _userId };
            }
            else
            {
                return base.CreateRoutValues();
            }
        }

        public override ActionResult Index(int? id)
        {
            if (_applicationId != 0)
            {
                return View(EntityCollection.Where(x => x.ApplicationId == _applicationId));
            }
            else if (_userId != 0)
            {
                return View(EntityCollection.Where(x => x.UserId == _userId));
            }
            else
            {
                return base.Index(id);
            }
        }
    }
}