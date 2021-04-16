using System.Web.Mvc;
using CentralSecurityProject.Common;

namespace CentralSecurityProject
{
    /// <summary>
    /// این کلاس این امکان را فراهم می آورد تا بتوان انواع فیلترهای
    /// لازم را بروی پروژه ام وی سی پیاده سازی نمود
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// این متد در تمامی عملیات های مربوط  به روتینگ ها
        /// فراخوانی می شود و فیلتر های لازم اعمال می گردد
        /// به عبارتی به ازای هر درخواستی که از سمت کلاینت به سمت
        /// سرور ارسال می شود این متد اجرا و فیلتر های لازم اعمال می گردد
        /// </summary>
        /// <param name="filters">فیلتر</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // بطور معمول از این فیلتر برای وب اپلیکیشن استفاده می شود ولی برای سایت ها استفاده نمی شود
            //filters.Add(new AuthorizeAttribute()); // با اعمال این فیلتر تمامی استفاده کنندگان از سایت می بایست لاگین کرده باشند
            filters.Add(new HandleErrorAttribute());
            filters.Add(new MyActionFilterAttribute()); // جهت اعمال فیلتر بروی تمامی اکشن متدها
        }
    }
}
