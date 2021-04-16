using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CentralSecurityProject.Controllers
{
    /// <summary>
    /// کلاس کنترلر مربوط به تغییر زبان 
    /// </summary>
    public class LanguageController : Controller
    {
        /// <summary>
        /// متد مربوط به تغییر زبان
        /// </summary>
        /// <param name="selectedlanguage"></param>
        /// <returns></returns>
        public ActionResult ChangeLanguage(string selectedlanguage)
        {
            if (selectedlanguage != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedlanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedlanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = selectedlanguage;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}