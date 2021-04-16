using System.Linq;
using System.Collections.Generic;

namespace CentralSecurityProject.App_Start
{
    public class ApplicationConfig
    {
        public static List<string> GetSchemas()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetTypes().
                Where(w => typeof(System.Web.Mvc.Controller).IsAssignableFrom(w) && w.Namespace.StartsWith("CentralSecurityProject.Controllers."))
                    .Select(s => s.Namespace.Replace("CentralSecurityProject.Controllers.", "").Split(new char[] { '.' }).Last()).Distinct().ToList();
        }
    }
}