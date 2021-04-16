using System.Web.Mvc;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به نقش کاربران
    /// </summary>
    public class UserRoleController : Controller
    {
        // GET: UserRole
        public ActionResult Index()
        {
            return View();
        }
    }
}