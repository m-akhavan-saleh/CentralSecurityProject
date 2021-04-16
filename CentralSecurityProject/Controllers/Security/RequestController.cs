using CentralSecurityProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CentralSecurityProject.Models.Security;
using CentralSecurityProject.ViewModels.Security;
using System.Data.Entity;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به درخواست کاربران
    /// </summary>
    [Authorize]
    public class RequestController : BaseController<Models.Security.RequestModel>
    {
        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        public RequestController()
        {
            MyInitialize();
        }

        /// <summary>
        /// متد مربوط به تنظیمات کنترلر درخواست
        /// </summary>
        private void MyInitialize()
        {
            ViewBag.Title = "درخواست کاربران";
            ViewBag.RequestTypes = new SelectList(_context.RequestTypeModels.Where(x => x.IsActive), "RequestTypeId", "RequestTypeName");
            ViewBag.RequestStatuses = new SelectList(_context.RequestStatusModels.Where(x => x.IsActive), "RequestStatusId", "RequestStatusName");
        }

        /// <summary>
        /// متد تنظیم نمونه مدل قبل از عملیات ایجاد
        /// </summary>
        /// <param name="instance">نمونه</param>
        public override void OnBeforePost(RequestModel instance)
        {
            instance.RequestStatusId = 1; // تنظیم درخواست
            base.OnBeforePost(instance);
        }

        /// <summary>
        /// متد مربوط به بازیابی اطلاعات درخواس
        /// </summary>
        /// <param name="id">شناسه</param>
        /// <returns></returns>
        public override ActionResult Index(int? id)
        {
            if (id != null && id != 0)
            {
                ViewBag.ID = id;
                ViewBag.ReqStatusId = _context.RequestModels.Where(x => x.RequestId == id).FirstOrDefault().RequestStatusId;
            }

            List<ViewModels.Security.RequestViewModel> reqVMList =
                    new List<ViewModels.Security.RequestViewModel>();

            if (HttpContext.Request.IsAuthenticated)
            {
                // به کمک این دو خط دستور شما می توانید اطلاعات کاربر ورودی به سیستم را بدست آورید
                ApplicationUser aspNetUser = _context.Users.FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                string iUserId = aspNetUser.Id;

                var subquary = from a in _context.RequestModels
                               join b in _context.RequestTypeModels on a.RequestTypeId equals b.RequestTypeId
                               join c in _context.RequestStatusModels on a.RequestStatusId equals c.RequestStatusId into tmp
                               from x in tmp.DefaultIfEmpty()
                               where a.InsertUserId == iUserId
                               select new
                               {
                                   RequestId = a.RequestId,
                                   RequestDate = a.RequestDate,
                                   RequestTypeId = a.RequestTypeId,
                                   RequestTypeName = b.RequestTypeName,
                                   RequestStatusId = a.RequestStatusId,
                                   RequestStatusName = x.RequestStatusName,
                                   ExpertId = a.ExpertId,
                                   ExpertDate = a.ExpertDate,
                                   Response = a.Response
                               };

                var quary = from a in subquary
                            join b in _context.UserModels on a.ExpertId equals b.UserId into tmp
                            from x in tmp.DefaultIfEmpty()
                            select new
                            {
                                RequestId = a.RequestId,
                                RequestDate = a.RequestDate,
                                RequestTypeId = a.RequestTypeId,
                                RequestTypeName = a.RequestTypeName,
                                RequestStatusId = a.RequestStatusId,
                                RequestStatusName = a.RequestStatusName,
                                ExpertId = a.ExpertId,
                                ExpertDate = a.ExpertDate,
                                ExpertUsername = x.UserName,
                                Response = a.Response
                            };

                foreach (var item in quary)
                {
                    ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                    reqvm.RequestId = item.RequestId;
                    reqvm.ID = item.RequestId;
                    reqvm.RequestDate = item.RequestDate;
                    reqvm.RequestTypeId = item.RequestTypeId;
                    reqvm.RequestTypeName = item.RequestTypeName;
                    reqvm.RequestStatusId = item.RequestStatusId;
                    reqvm.RequestStatusName = item.RequestStatusName;
                    reqvm.ExpertId = item.ExpertId.GetValueOrDefault(0);
                    reqvm.ExpertUsername = item.ExpertUsername;
                    reqvm.ExpertDate = item.ExpertDate.GetValueOrDefault();
                    reqvm.Response = item.Response;

                    reqVMList.Add(reqvm);
                }
            }

            return View(reqVMList);
        }

        /// <summary>
        /// بررسی و ارجاع درخواست
        /// </summary>
        /// <returns></returns>
        [Common.MyRoleAuthorize(RoleName = "administrator")]
        public ActionResult ReferralRequest()
        {
            ViewBag.Title = "بررسی و ارجاع درخواست کاربران";

            List<RequestViewModel> reqVMList = new List<RequestViewModel>();

            var quary = from a in _context.RequestModels
                        join b in _context.RequestTypeModels
                        on a.RequestTypeId equals b.RequestTypeId
                        join c in _context.RequestStatusModels
                        on a.RequestStatusId equals c.RequestStatusId
                        join d in _context.Users
                        on a.InsertUserId equals d.Id into tmp
                        from x in tmp
                        where a.RequestStatusId == 1 || a.RequestStatusId == 2 // درخواست ها در وضعیت تنظیم 
                        select new
                        {
                            RequestId = a.RequestId,
                            ID = a.RequestId,
                            RequestDate = a.RequestDate,
                            RequestUserName = x.UserName,
                            RequestTypeId = a.RequestTypeId,
                            RequestTypeName = b.RequestTypeName,
                            RequestStatusId = a.RequestStatusId,
                            RequestStatusName = c.RequestStatusName
                        };

            foreach (var item in quary)
            {
                ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                reqvm.RequestId = item.RequestId;
                reqvm.ID = item.ID;
                reqvm.RequestDate = item.RequestDate;
                reqvm.RequestTypeId = item.RequestTypeId;
                reqvm.RequestTypeName = item.RequestTypeName;
                reqvm.RequestStatusId = item.RequestStatusId;
                reqvm.RequestStatusName = item.RequestStatusName;
                reqvm.RequestUsername = item.RequestUserName;

                reqVMList.Add(reqvm);
            }

            return View(reqVMList);
        }

        /// <summary>
        /// ارجاع درخواست
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.MyRoleAuthorize(RoleName = "administrator")]
        public ActionResult Referral(int id)
        {
            ViewBag.Title = "ارجاع درخواست";

            ViewBag.Experts = new SelectList(_context.UserModels.Where(x => x.IsActive), "UserId", "Username");

            List<ViewModels.Security.RequestViewModel> reqVMList =
                   new List<ViewModels.Security.RequestViewModel>();

            var quary = from a in _context.RequestModels
                        join b in _context.RequestTypeModels
                        on a.RequestTypeId equals b.RequestTypeId
                        join c in _context.RequestStatusModels
                        on a.RequestStatusId equals c.RequestStatusId
                        join d in _context.Users
                        on a.InsertUserId equals d.Id into tmp
                        from x in tmp
                        where a.RequestStatusId == 1 || a.RequestStatusId == 2 // درخواست ها در وضعیت تنظیم و یا تائید 
                        select new
                        {
                            RequestId = a.RequestId,
                            ID = a.RequestId,
                            RequestDate = a.RequestDate,
                            RequestUserName = x.UserName,
                            RequestTypeId = a.RequestTypeId,
                            RequestTypeName = b.RequestTypeName,
                            RequestStatusName = c.RequestStatusName,
                            RequestDescription = a.RequestDescription
                        };

            foreach (var item in quary)
            {
                ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                reqvm.RequestId = item.RequestId;
                reqvm.ID = item.ID;
                reqvm.RequestDate = item.RequestDate;
                reqvm.RequestTypeId = item.RequestTypeId;
                reqvm.RequestTypeName = item.RequestTypeName;
                reqvm.RequestStatusName = item.RequestStatusName;
                reqvm.RequestUsername = item.RequestUserName;
                reqvm.RequestDescription = item.RequestDescription;

                reqVMList.Add(reqvm);
            }

            return View(reqVMList.ToList().Where(x => x.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// ارجاع درخواست
        /// </summary>
        /// <param name="instance">نمونه مدل</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Referral(RequestViewModel instance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequestModel obj = new RequestModel();
                    obj = _context.RequestModels.ToList().Where(x => x.RequestId == instance.RequestId).FirstOrDefault();
                    obj.ExpertId = instance.ExpertId;
                    obj.ExpertDate = DateTime.Now;
                    obj.RequestStatusId = 4;
                    _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("ReferralRequest", CreateRoutValues());
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
        /// تائید درخواست
        /// </summary>
        /// <param name="id">شماره درخواست</param>
        /// <returns></returns>
        public ActionResult Approve(int id)
        {
            RequestModel req = new RequestModel();
            try
            {
                if (ModelState.IsValid)
                {
                    req = _context.RequestModels.Where(x => x.RequestId == id).FirstOrDefault();
                    req.RequestStatusId = 2; // وضعیت تائید درخواست
                    _context.Entry(req).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("ReferralRequest", CreateRoutValues());
                }
                else
                {
                    return View(req);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                return View(req);
            }
        }

        /// <summary>
        /// رد درخواست
        /// </summary>
        /// <param name="id">شماره درخواست</param>
        /// <returns></returns>
        public ActionResult Cancel(int id)
        {
            RequestModel req = new RequestModel();
            try
            {
                if (ModelState.IsValid)
                {
                    req = _context.RequestModels.Where(x => x.RequestId == id).FirstOrDefault();
                    req.RequestStatusId = 3; // وضعیت تائید درخواست
                    _context.Entry(req).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("ReferralRequest", CreateRoutValues());
                }
                else
                {
                    return View(req);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                return View(req);
            }
        }

        /// <summary>
        /// متد مربوط به بازیابی اطلاعات تاریخچه درخواست
        /// </summary>
        /// <param name="id">شماره درخواست</param>
        /// <returns></returns>
        public ActionResult History(int id)
        {
            ViewBag.Title = "مشاهده تاریخچه درخواست";
            ViewBag.ID = id;
            ViewBag.ReqStatusId = _context.RequestModels.Where(x => x.RequestId == id).FirstOrDefault().RequestStatusId;

            List<ViewModels.Security.RequestViewModel> reqVMList = new List<ViewModels.Security.RequestViewModel>();

            if (HttpContext.Request.IsAuthenticated)
            {
                // به کمک این دو خط دستور شما می توانید اطلاعات کاربر ورودی به سیستم را بدست آورید
                ApplicationUser aspNetUser = _context.Users.FirstOrDefault(f => f.UserName == HttpContext.User.Identity.Name);
                string iUserId = aspNetUser.Id;

                var subquary = from a in _context.RequestHistoryModels
                               join b in _context.RequestTypeModels on a.RequestTypeId equals b.RequestTypeId
                               join c in _context.RequestStatusModels on a.RequestStatusId equals c.RequestStatusId into tmp
                               from x in tmp.DefaultIfEmpty()
                               where a.RequestId == id
                               select new
                               {
                                   RequestId = a.RequestId,
                                   RequestDate = a.RequestDate,
                                   RequestTypeId = a.RequestTypeId,
                                   RequestTypeName = b.RequestTypeName,
                                   RequestStatusId = a.RequestStatusId,
                                   RequestStatusName = x.RequestStatusName,
                                   ExpertId = a.ExpertId,
                                   ExpertDate = a.ExpertDate,
                                   RequestHistoryDate = a.RequestHistoryDate,
                                   Response = a.Response
                               };

                var quary = from a in subquary
                            join b in _context.UserModels on a.ExpertId equals b.UserId into tmp
                            from x in tmp.DefaultIfEmpty()
                            select new
                            {
                                RequestId = a.RequestId,
                                RequestDate = a.RequestDate,
                                RequestTypeId = a.RequestTypeId,
                                RequestTypeName = a.RequestTypeName,
                                RequestStatusId = a.RequestStatusId,
                                RequestStatusName = a.RequestStatusName,
                                ExpertId = a.ExpertId,
                                ExpertDate = a.ExpertDate,
                                ExpertUsername = x.UserName,
                                RequestHistoryDate = a.RequestHistoryDate,
                                Response = a.Response
                            };

                foreach (var item in quary)
                {
                    ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                    reqvm.RequestId = item.RequestId;
                    reqvm.ID = item.RequestId;
                    reqvm.RequestDate = item.RequestDate;
                    reqvm.RequestTypeId = item.RequestTypeId;
                    reqvm.RequestTypeName = item.RequestTypeName;
                    reqvm.RequestStatusId = item.RequestStatusId;
                    reqvm.RequestStatusName = item.RequestStatusName;
                    reqvm.ExpertId = item.ExpertId.GetValueOrDefault(0);
                    reqvm.ExpertUsername = item.ExpertUsername;
                    reqvm.ExpertDate = item.ExpertDate.GetValueOrDefault();
                    reqvm.RequestHistoryDate = item.RequestHistoryDate;
                    reqvm.Response = item.Response;

                    reqVMList.Add(reqvm);
                }
            }

            return View("History", reqVMList);
        }

        /// <summary>
        /// بررسی و پاسخ درخواست
        /// </summary>
        /// <returns></returns>
        [Common.MyRoleAuthorize(RoleName = "administrator")]
        public ActionResult ResponseRequest()
        {
            ViewBag.Title = "بررسی و پاسخ درخواست کاربران";

            List<RequestViewModel> reqVMList = new List<RequestViewModel>();

            var quary = from a in _context.RequestModels
                        join b in _context.RequestTypeModels
                        on a.RequestTypeId equals b.RequestTypeId
                        join c in _context.RequestStatusModels
                        on a.RequestStatusId equals c.RequestStatusId
                        join d in _context.Users
                        on a.InsertUserId equals d.Id into tmp
                        from x in tmp
                        where a.RequestStatusId == 4 // درخواست ها در وضعیت تخصیص کارشناس 
                        select new
                        {
                            RequestId = a.RequestId,
                            ID = a.RequestId,
                            RequestDate = a.RequestDate,
                            RequestUserName = x.UserName,
                            RequestTypeId = a.RequestTypeId,
                            RequestTypeName = b.RequestTypeName,
                            RequestStatusId = a.RequestStatusId,
                            RequestStatusName = c.RequestStatusName
                        };

            foreach (var item in quary)
            {
                ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                reqvm.RequestId = item.RequestId;
                reqvm.ID = item.ID;
                reqvm.RequestDate = item.RequestDate;
                reqvm.RequestTypeId = item.RequestTypeId;
                reqvm.RequestTypeName = item.RequestTypeName;
                reqvm.RequestStatusId = item.RequestStatusId;
                reqvm.RequestStatusName = item.RequestStatusName;
                reqvm.RequestUsername = item.RequestUserName;

                reqVMList.Add(reqvm);
            }

            return View(reqVMList);
        }

        /// <summary>
        /// ارجاع درخواست
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common.MyRoleAuthorize(RoleName = "administrator")]
        [ActionName("Response")]
        public ActionResult RequestResponse(int id)
        {
            ViewBag.Title = "پاسخ درخواست";

            ViewBag.Experts = new SelectList(_context.UserModels.Where(x => x.IsActive), "UserId", "Username");

            List<ViewModels.Security.RequestViewModel> reqVMList =
                   new List<ViewModels.Security.RequestViewModel>();

            var quary = from a in _context.RequestModels
                        join b in _context.RequestTypeModels
                        on a.RequestTypeId equals b.RequestTypeId
                        join c in _context.RequestStatusModels
                        on a.RequestStatusId equals c.RequestStatusId
                        join d in _context.Users
                        on a.InsertUserId equals d.Id into tmp
                        from x in tmp
                        where a.RequestId == id
                        select new
                        {
                            RequestId = a.RequestId,
                            ID = a.RequestId,
                            RequestDate = a.RequestDate,
                            RequestUserName = x.UserName,
                            RequestTypeId = a.RequestTypeId,
                            RequestTypeName = b.RequestTypeName,
                            RequestStatusName = c.RequestStatusName,
                            RequestDescription = a.RequestDescription
                        };

            foreach (var item in quary)
            {
                ViewModels.Security.RequestViewModel reqvm = new ViewModels.Security.RequestViewModel();
                reqvm.RequestId = item.RequestId;
                reqvm.ID = item.ID;
                reqvm.RequestDate = item.RequestDate;
                reqvm.RequestTypeId = item.RequestTypeId;
                reqvm.RequestTypeName = item.RequestTypeName;
                reqvm.RequestStatusName = item.RequestStatusName;
                reqvm.RequestUsername = item.RequestUserName;
                reqvm.RequestDescription = item.RequestDescription;

                reqVMList.Add(reqvm);
            }

            return View(reqVMList.ToList().Where(x => x.ID == id).FirstOrDefault());
        }

        /// <summary>
        /// پاسخ درخواست
        /// </summary>
        /// <param name="instance">نمونه مدل</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Response")]
        public ActionResult RequestResponse(RequestViewModel instance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RequestModel obj = new RequestModel();
                    obj = _context.RequestModels.ToList().Where(x => x.RequestId == instance.RequestId).FirstOrDefault();
                    obj.Response = instance.Response;
                    obj.RequestStatusId = 5;
                    _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("ResponseRequest", CreateRoutValues());
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
    }
}