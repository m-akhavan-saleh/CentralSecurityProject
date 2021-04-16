using System.Web.Mvc;

namespace CentralSecurityProject.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default Or Default/Index http://localhost:6119/Default
        public string Index()
        {
            return "All Subjects";
        }

        // GET: http://localhost:6119/Default/FirstMethod?param=10
        public string FirstMethod(string param)
        {
            return $"مقدار پارامتر ورودی {param}";
        }

        // GET: http://localhost:6119/Default/SecondMethod/10
        public string SecondMethod(int id)
        {
            return $"مقدار پارامتر ورودی id = {id}";
        }

        // GET: http://localhost:6119/Default/ThirdMethod?a=10&b=20
        public string ThirdMethod(int a,int b)
        {
            int result = a + b;
            return $"مجموع مقادیر پارامترهای ورودی a+b = {result}";
        }

        // GET: http://localhost:6119/default/default
        public ActionResult Default()
        {
            return View();
        }
    }
}