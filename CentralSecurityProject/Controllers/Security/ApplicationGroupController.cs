using CentralSecurityProject.Models;
using CentralSecurityProject.Models.Security;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CentralSecurityProject.Controllers.Security
{
    public class ApplicationGroupController : Controller
    {
        /// <summary>
        /// ایجاد یک نمونه از زمینه پایگاه داده
        /// </summary>
        private Models.ApplicationDbContext context;

        /// <summary>
        /// ایجاد کلاس سازنده پیش فرض
        /// </summary>
        public ApplicationGroupController()
        {
            context = new Models.ApplicationDbContext(); // ساخت یک نمونه از زمینه بانک اطلاعاتی
        }

        /// <summary>
        /// متد فراخوانی اطلاعات
        /// </summary>
        /// <returns></returns>
        [HttpGet] // Default HttpGet
        public ActionResult Index(int? id)
        {
            ViewBag.ID = null;
            if (id != null && id != 0)
            {
                ViewBag.ID = id;
            }
            ViewBag.Title = "گروه بندی سیستم ها";
            Models.ApplicationDbContext context = new Models.ApplicationDbContext();
            return View(context.ApplicationGroupModels);
        }

        /// <summary>
        /// متد فراخوانی فرم
        /// ثبت اطلاعات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize] // تعریف خصوصیت بالای سر هر یک از متد ها بدین معنی است که اجرای آن منوط به ورود کاربر به سیستم می باشد
        [Common.MyClaimAuthorize(ClaimType = "ApplicationGroup", ClaimValue = "Create")] // بررسی وضعیت اعلام مجوز دسترسی
        public ActionResult Create()
        {
            ViewBag.Title = "تعریف گروه بندی زیر سیستم ها";
            return View();
        }

        /// <summary>
        /// متد ذخیره سازی
        /// اطلاعات ثبت شده
        /// </summary>
        /// <param name="instance">نمونه اطلاعات گروه بندی سیستم ها</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // جهت جلوگیری از درخواست های خارح از سایت
        // [Bind(Include = "")] تنها خصوصیت های قید شده قابل اعمال باشد
        // [Bind(Exclude = "")] تنها غیر از خصوصیت های قید شده قابل اعمال باشد
        [Authorize]
        [Common.MyClaimAuthorize(ClaimType = "ApplicationGroup", ClaimValue = "Create")] // بررسی وضعیت اعلام مجوز دسترسی
        public ActionResult Create([Bind(Include = "ApplicationGroupId,ApplicationGroupName,IsActive")] Models.Security.ApplicationGroupModel instance) // Over Posting
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                // به کمک این دو خط دستور شما می توانید اطلاعات کاربر ورودی به سیستم را بدست آورید
                //ApplicationUser aspNetUser = context.Users.Include(u => u.User_List).FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                ApplicationUser aspNetUser = context.Users.FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                UserModel user = aspNetUser.User_List.FirstOrDefault();
            }

            try
            {
                /*
                if (ModelState.IsValid)
                {
                    ViewBag.Title = "تعریف گروه بندی زیر سیستم ها";
                    context.ApplicationGroupModels.Add(instance);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(instance);
                }
                */

                if (TryValidateModel(instance))
                {
                    ViewBag.Title = "تعریف گروه بندی زیر سیستم ها";
                    context.ApplicationGroupModels.Add(instance);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "فیلدهای اطلاعاتی مقدار دهی شده است که نمی بایست از طرف کاربر مقدار به آن فیلد ها داده می شد.");
                    return View(instance);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// متد مربوط به فراخوانی  
        /// اطلاعات جهت ویرایش آن
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "ویرایش اطلاعات گروه بندی زیر سیستم ها";
            return View(context.ApplicationGroupModels.FirstOrDefault(x => x.ApplicationGroupId == id)); // Lambda Experssion (x => x.###) عبارت لاندا
        }

        /// <summary>
        /// متد مربوط به ذخیره سازی
        /// اطلاعات ویرایش شده
        /// </summary>
        /// <param name="instance">نمونه مدل</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // جهت جلوگیری از درخواست های خارح از سایت
        [Authorize]
        public ActionResult Edit(Models.Security.ApplicationGroupModel instance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Title = "ویرایش اطلاعات گروه بندی زیر سیستم ها";
                    context.Entry(instance).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(instance);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// متد فراخوانی اطلاعات
        /// جهت حذف آن از بانک اطلاعاتی
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        [Authorize]
        public ActionResult Delete(int id)
        {
            ViewBag.Title = "حذف اطلاعات مربوط به گروه بندی زیر سیستم ها";
            return View(context.ApplicationGroupModels.FirstOrDefault(x => x.ApplicationGroupId == id));
        }

        /// <summary>
        /// متد جهت حذف اطلاعات از داخل بانک اطلاعاتی
        /// </summary>
        /// <param name="instance">نمونه مدل</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // جهت جلوگیری از درخواست های خارح از سایت
        [Authorize] // تعریف خصوصیت بالای سر هر یک از متد ها بدین معنی است که اجرای آن منوط به ورود کاربر به سیستم می باشد
        public ActionResult Delete(Models.Security.ApplicationGroupModel instance)
        {
            try
            {
                context.Entry(instance).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// متد مربوط به فراخوانی  
        /// اطلاعات جهت نمایش آن
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        public ActionResult Details(int id)
        {
            ViewBag.Title = "اطلاعات گروه بندی زیر سیستم ها";
            return View(context.ApplicationGroupModels.FirstOrDefault(x => x.ApplicationGroupId == id));
        }
    }
}