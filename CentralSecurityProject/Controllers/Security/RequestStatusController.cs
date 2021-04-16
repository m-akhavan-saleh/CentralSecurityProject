using System.Web.Mvc;
using CentralSecurityProject.DataAccess;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به وضعیت درخواست
    /// </summary>
    public class RequestStatusController : Controller
    {
        /// <summary>
        /// تعریف سرویس جهت انجام عملیات
        /// </summary>
        private IBaseRepository<Models.Security.RequestStatusModel> _service;

        /// <summary>
        /// تعریف سازنده پیش فرض
        /// </summary>
        public RequestStatusController()
        {
            _service = new BaseRepository<Models.Security.RequestStatusModel>(new Models.ApplicationDbContext());
        }

        /// <summary>
        /// تعریف سازنده پیش فرض
        /// </summary>
        /// <param name="service">نام سرویس</param>
        public RequestStatusController(IBaseRepository<Models.Security.RequestStatusModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// متد مربوط به بازیابی اطلاعات
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        /// <summary>
        /// متد بازیابی اطلاعات
        /// به همراه انتخاب رکورد مورد نظر
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        public ActionResult Select(int id)
        {
            ViewBag.ID = id;
            return View("Index", _service.GetAll());
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
        /// متد ذخیره سازی اطلاعات ثبت شده
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken] // جهت کنترل و جلوگیری از درخواست های ارسالی از خارج سایت
        public virtual ActionResult Create(Models.Security.RequestStatusModel instance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Insert(instance);
                    _service.SaveChanges();

                    return RedirectToAction("Index");
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
            return View(_service.GetByID(id));
        }

        /// <summary>
        /// متد مربوط به حذف اطلاعات
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(Models.Security.RequestStatusModel instance)
        {
            try
            {
                _service.Delete(instance.RequestStatusId);
                _service.SaveChanges();
                return RedirectToAction("Index");
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
            return View(_service.GetByID(id));
        }

        /// <summary>
        /// متد ذخیره سازی اطلاعات ویرایش شده
        /// </summary>
        /// <param name="instance">نمونه اطلاعات</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Security.RequestStatusModel instance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _service.Update(instance);
                    _service.SaveChanges();

                    return RedirectToAction("Index");
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
        /// متد مربوط به نمایش اطلاعات
        /// </summary>
        /// <param name="id">شناسه موجودیت</param>
        /// <returns></returns>
        // Default HttpGet
        public virtual ActionResult Details(int id)
        {
            return View(_service.GetByID(id));
        }
    }
}