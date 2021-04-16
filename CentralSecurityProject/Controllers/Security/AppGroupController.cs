using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس مربوط به گروه دسترسی
    /// هریک از برنامه ها / زیر سیستم ها
    /// </summary>
    public class AppGroupController : BaseController<Models.Security.AppGroupModel>
    {
        protected override void Initialize(RequestContext requestContext)
        {
            ViewBag.Title = "گروه دسترسی";
            ViewBag.Applications = new SelectList(_context.ApplicationModels.Where(x => x.IsActive), "ApplicationId", "ApplicationName");
            base.Initialize(requestContext);
        }
    }
}