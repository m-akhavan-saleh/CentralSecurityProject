using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using CentralSecurityProject.Models;
using CentralSecurityProject.Models.Security;
using System;

namespace CentralSecurityProject.Controllers
{
    public class BaseController<TEntity> : Controller
        where TEntity : Models.Security.SecurityBaseModel
    {
        /// <summary>
        /// ایجاد یک نمونه از زمینه پایگاه داده
        /// </summary>
        protected Models.ApplicationDbContext _context;

        /// <summary>
        /// ایجاد یک نمونه از مجموعه مدیریت
        /// </summary>
        private DbSet<TEntity> _entityCollection;

        /// <summary>
        /// تعریف یک مجموعه موجودیت
        /// </summary>
        protected DbSet<TEntity> EntityCollection // Singletone
        {
            get
            {
                if (_entityCollection == null)
                    _entityCollection = _context.Set<TEntity>();
                return _entityCollection;
            }
        }

        /// <summary>
        /// ایجاد کلاس سازنده پیش فرض
        /// </summary>
        public BaseController()
        {
            _context = new Models.ApplicationDbContext(); // ساخت یک نمونه از زمینه بانک اطلاعاتی
        }

        /// <summary>
        /// متد جهت ساخت مسیر
        /// کنترلر مورد نظر
        /// </summary>
        /// <returns>QueryString</returns>
        protected virtual object CreateRoutValues()
        {
            return null;
        }

        /// <summary>
        /// متد فراخوانی اطلاعات
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        public virtual ActionResult Index(int? id)
        {
            ViewBag.ID = null;
            if (id != null && id != 0)
            {
                ViewBag.ID = id;
            }
            return View(EntityCollection);
        }

        /// <summary>
        /// متد فراخوانی اطلاعات بر اساس
        /// شناسه هریک از جداول اطلاعاتی
        /// </summary>
        /// <param name="id">شناسه جدول</param>
        /// <returns></returns>
        protected virtual TEntity Single(long id)
        {
            return EntityCollection.ToList().Where(x => x.ID == id).FirstOrDefault();
        }

        /// <summary>
        /// متد فراخوانی فرم
        /// ثبت اطلاعات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// این متد جهت رفع مشکل فایل های پست شده به سمت سرور می باشد
        /// </summary>
        /// <param name="instance"></param>
        public virtual void OnBeforePost(TEntity instance)
        {
            // پیاده سازی این متد در سمت کلاس های ارث برده شده می باشد
            // ...
        }

        /// <summary>
        /// متد ذخیره سازی اطلاعات ثبت شده
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // جهت کنترل و جلوگیری از درخواست های ارسالی از خارج سایت
        public virtual ActionResult Create(TEntity instance)
        {
            try
            {
                if (HttpContext.Request.IsAuthenticated)
                {
                    ApplicationUser aspNetUser = _context.Users.FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                    instance.InsertUserId = aspNetUser.Id;
                    instance.InsertDate = System.DateTime.Now;
                }

                OnBeforePost(instance);

                if (ModelState.IsValid)
                {
                    EntityCollection.Add(instance);
                    _context.SaveChanges();

                    return RedirectToAction("Index", CreateRoutValues());
                }
                else
                {
                    return View(instance);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                return View(instance);
            }
        }

        /// <summary>
        /// متد مربوط به فراخوانی  
        /// اطلاعات جهت ویرایش آن
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        public virtual ActionResult Edit(int id)
        {
            return View(EntityCollection.ToList().Where(x => x.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// متد ذخیره سازی اطلاعات ویرایش شده
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TEntity instance)
        {
            try
            {
                if (HttpContext.Request.IsAuthenticated)
                {
                    ApplicationUser aspNetUser = _context.Users.FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                    instance.EditUserId = aspNetUser.Id;
                    instance.EditDate = System.DateTime.Now;
                }

                OnBeforePost(instance);

                if (ModelState.IsValid)
                {
                    _context.Entry(instance).State = EntityState.Modified;
                    _context.SaveChanges();
                    
                    return RedirectToAction("Index", CreateRoutValues());
                }
                else
                {
                    return View(instance);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                return View(instance);
            }
        }

        /// <summary>
        /// متد مربوط به فراخوانی اطلاعات به جهت حذف
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        // Default HttpGet
        public virtual ActionResult Delete(int id)
        {
            return View(EntityCollection.ToList().Where(x => x.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// متد مربوط به حذف اطلاعات
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(TEntity instance)
        {
            try
            {
                _context.Entry(instance).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction("Index", CreateRoutValues());
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                return View(instance);
            }
        }

        /// <summary>
        /// متد مربوط به نمایش اطلاعات
        /// </summary>
        /// <param name="id">شناسه موجودیت</param>
        /// <returns></returns>
        // Default HttpGet
        public virtual ActionResult Details(long id)
        {
            return View(EntityCollection.ToList().Where(x => x.ID == id).FirstOrDefault());
        }
    }
}