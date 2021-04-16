using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به برنامه/زیر سیستم
    /// </summary>
    [Authorize] // کلیه متدهای کلاس فوق نیاز به ورود کاربر به سیستم می باشد
    public class ApplicationController : BaseController<Models.Security.ApplicationModel>
    {
        /// <summary>
        /// شناسه گروه بندی برنامه/زیر سیستم
        /// </summary>
        private int _applicationGroupId = 0;
        /// <summary>
        /// متد مربوط به تنظیمات اولیه خود کنترلر
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            if (!string.IsNullOrEmpty(requestContext.HttpContext.Request.QueryString["ApplicationGroupId"]))
            {
                int.TryParse(requestContext.HttpContext.Request.QueryString["ApplicationGroupId"], out _applicationGroupId);
            }
            ViewBag.ApplicationGroupId = _applicationGroupId;
            ViewBag.Title = "برنامه/زیر سیستم";
            ViewBag.ApplicationGroups = new SelectList(_context.ApplicationGroupModels.Where(x => x.IsActive).
                Where(x => x.ApplicationGroupId == _applicationGroupId || _applicationGroupId == 0), "ApplicationGroupId", "ApplicationGroupName");
            base.Initialize(requestContext);
        }

        /// <summary>
        /// تعریف مسیر کنترلر مورد نظر
        /// </summary>
        /// <returns>QueryString</returns>
        protected override object CreateRoutValues()
        {
            if (_applicationGroupId != 0)
            {
                return new { ApplicationGroupId = _applicationGroupId };
            }
            else
            {
                return base.CreateRoutValues();
            }
        }

        /// <summary>
        /// متد مربوط به نمایش لیست اطلاعات 
        /// مربوط به برنامه ها/زیر سیستم ها
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous] // این خصوصیت بدین معنی می باشد که نیاز به ورود کاربر به سیستم نمی باشد و برای عموم آزاد است
        // خصوصیت ها در کل امکان در آنها وجود دارد که مشخص می کند که آیا شما می توانید از آن ارث بری داشته باشید یا نه
        public override ActionResult Index(int? id)
        {
            // روش اول : احراز هویت
            if (HttpContext.Request.IsAuthenticated) // کنترل اینکه آیا کاربر فوق وارد سیستم شده است یا خیر ؟
            {
                // ...
            }

            // روش دوم : احراز هویت
            if (HttpContext.User.Identity.IsAuthenticated) // کنترل اینکه کاربر فوق احراز هویت شده است یا خیر ؟
            {
                // ...
            }

            if (_applicationGroupId != 0)
            {
                // First : With Entity Model
                //return View(EntityCollection.Where(x => x.ApplicationGroupId == _applicationGroupId));

                // Second : With View Model
                List<ViewModels.Security.ApplicationViewModel> appVMList =
                    new List<ViewModels.Security.ApplicationViewModel>();
                var quary = from a in _context.ApplicationModels
                            join b in _context.ApplicationGroupModels
                            on a.ApplicationGroupId equals b.ApplicationGroupId into tmp
                            from x in tmp
                            where x.ApplicationGroupId == _applicationGroupId
                            select new
                            {
                                ApplicationId = a.ApplicationId,
                                ID = a.ApplicationId,
                                ApplicationNum = a.ApplicationNum,
                                ApplicationName = a.ApplicationName,
                                ApplicationGroupName = x.ApplicationGroupName,
                                IsActive = a.IsActive
                            };
                foreach (var item in quary)
                {
                    ViewModels.Security.ApplicationViewModel appvm = new ViewModels.Security.ApplicationViewModel();
                    appvm.ApplicationId = item.ApplicationId;
                    appvm.ID = item.ID;
                    appvm.ApplicationNum = item.ApplicationNum;
                    appvm.ApplicationName = item.ApplicationName;
                    appvm.ApplicationGroupName = item.ApplicationGroupName;
                    appvm.IsActive = item.IsActive;

                    appVMList.Add(appvm);
                }
                return View(appVMList);
            }
            else
            {
                // First : With Entity Model
                //return base.Index(id);

                // Second : With View Model
                List<ViewModels.Security.ApplicationViewModel> appVMList =
                    new List<ViewModels.Security.ApplicationViewModel>();
                var quary = from a in _context.ApplicationModels
                          join b in _context.ApplicationGroupModels
                          on a.ApplicationGroupId equals b.ApplicationGroupId into tmp
                          from x in tmp
                            select new
                            {
                                ApplicationId = a.ApplicationId,
                                ID = a.ApplicationId,
                                ApplicationNum = a.ApplicationNum,
                                ApplicationName = a.ApplicationName,
                                ApplicationGroupName = x.ApplicationGroupName,
                                IsActive = a.IsActive
                            };
                foreach (var item in quary)
                {
                    ViewModels.Security.ApplicationViewModel appvm = new ViewModels.Security.ApplicationViewModel();
                    appvm.ApplicationId = item.ApplicationId;
                    appvm.ID = item.ID;
                    appvm.ApplicationNum = item.ApplicationNum;
                    appvm.ApplicationName = item.ApplicationName;
                    appvm.ApplicationGroupName = item.ApplicationGroupName;
                    appvm.IsActive = item.IsActive;

                    appVMList.Add(appvm);
                }
                return View(appVMList);
            }
        }

        /// <summary>
        /// متد جستجو در برنامه های کاربردی سیستم جامع
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Search()
        {
            ViewBag.Title = "جستجوی برنامه/زیر سیستم";
            return View();
        }

        /// <summary>
        /// متد جستجو در عنوان برنامه های کاربردی سیستم جامع
        /// </summary>
        /// <param name="ApplicationName">عنوان برنامه</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult GoSearch(string ApplicationName)
        {
            ViewBag.Title = "نتیجه جستجو";
            ViewBag.SearchText = ApplicationName;

            List<ViewModels.Security.ApplicationViewModel> appVMList =
                    new List<ViewModels.Security.ApplicationViewModel>();
            var quary = from a in _context.ApplicationModels
                        join b in _context.ApplicationGroupModels
                        on a.ApplicationGroupId equals b.ApplicationGroupId into tmp
                        from x in tmp
                        where a.ApplicationName.Contains(ApplicationName)
                        select new
                        {
                            ApplicationId = a.ApplicationId,
                            ID = a.ApplicationId,
                            ApplicationNum = a.ApplicationNum,
                            ApplicationName = a.ApplicationName,
                            ApplicationGroupName = x.ApplicationGroupName,
                            IsActive = a.IsActive
                        };
            foreach (var item in quary)
            {
                ViewModels.Security.ApplicationViewModel appvm = new ViewModels.Security.ApplicationViewModel();
                appvm.ApplicationId = item.ApplicationId;
                appvm.ID = item.ID;
                appvm.ApplicationNum = item.ApplicationNum;
                appvm.ApplicationName = item.ApplicationName;
                appvm.ApplicationGroupName = item.ApplicationGroupName;
                appvm.IsActive = item.IsActive;

                appVMList.Add(appvm);
            }

            return PartialView("SearchResult", appVMList);
        }
    }
}