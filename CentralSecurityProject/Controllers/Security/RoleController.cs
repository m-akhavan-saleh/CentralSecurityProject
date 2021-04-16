using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using CentralSecurityProject.Models.Security;
using System.Data.Entity;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به نقش
    /// </summary>
    public class RoleController : BaseController<Models.Security.RoleModel>
    {
        /// <summary>
        /// ایجاد کلاس سازنده پیش فرض
        /// </summary>
        public RoleController()
        {
            MyInitialize();
        }

        /// <summary>
        /// متد مربوط به تنظیمات کنترلر نقش
        /// </summary>
        private void MyInitialize()
        {
            ViewBag.Title = "نقش";
        }

        /// <summary>
        /// متد فراخوانی کاربران
        /// </summary>
        /// <returns></returns>
        private ICollection<UserModel> GetUsers(ICollection<UserModel> users)
        {
            UserModel model = new UserModel();
            ICollection<UserModel> list;
            Models.ApplicationDbContext context = new Models.ApplicationDbContext();
            list = context.UserModels.Where(x => x.IsActive).ToArray();
            if (users != null)
            {
                foreach (var item in users)
                {
                    list.FirstOrDefault(x => x.UserId == item.UserId).Selected = item.Selected;
                }
            }
            return list;
        }

        /// <summary>
        /// متد مربوط به ایجاد
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [ActionName("CreateRWU")] // Default Action Name : CreateWithDetail
        [HttpGet]
        public ActionResult CreateWithDetail()
        {
            var model = new Models.Security.RoleModel();
            model.User_List = GetUsers(null);
            return View("Create", model);
        }

        /// <summary>
        /// متد مربوط به ذخیره کردن اطلاعات
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        [ActionName("CreateRWU")] // Default Action Name : CreateWithDetail
        [HttpPost]
        public ActionResult CreateWithDetail(RoleModel instance, System.Collections.Generic.ICollection<Models.Security.UserModel> users)
        {
            try
            {
                foreach (var item in ModelState.Where(x => x.Key.Contains("[")))
                {
                    ModelState[item.Key].Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    EntityCollection.Add(instance);
                    _context.SaveChanges();

                    _context.Database.ExecuteSqlCommand("Delete Security.tbUserRole Where RoleId={0}", instance.RoleId);

                    foreach (var item in users)
                    {
                        if (item.Selected)
                        {
                            _context.Database.ExecuteSqlCommand("Insert Into Security.tbUserRole(UserId,RoleId) Values({0},{1})", item.UserId, instance.RoleId);
                        }
                    }

                    return RedirectToAction("Index", CreateRoutValues());
                }
                else
                {
                    instance.User_List = GetUsers(users);
                    return View("Create", instance);
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", Common.MyErrorHandler.TranslateErrorMessage(ex));
                instance.User_List = GetUsers(users);
                return View("Create", instance);
            }
        }

        public override ActionResult Index(int? id)
        {
            Models.ApplicationDbContext context = new Models.ApplicationDbContext();
            ViewBag.Users = null;
            if (id != null)
            {
                var users = context.RoleModels.Where(x => x.RoleId == id).FirstOrDefault().User_List;
                ViewBag.Users = users;
            }
            return base.Index(id);
        }
    }
}