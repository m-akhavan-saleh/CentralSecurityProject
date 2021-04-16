using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس مربوط به تعریف
    /// پارامترهای برنامه
    /// </summary>
    public class AppParameterController : BaseController<Models.Security.AppParameterModel>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public AppParameterController() // ctor tab tab [Default For Create Constructors]
        {
        }

        protected override void Initialize(RequestContext requestContext)
        {
            ViewBag.Title = "پارامتر برنامه/زیر سیستم";
            ViewBag.Applications = new SelectList(_context.ApplicationModels.Where(x => x.IsActive), "ApplicationId", "ApplicationName");
            base.Initialize(requestContext);
        }
    }
}