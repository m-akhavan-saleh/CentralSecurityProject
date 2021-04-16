using System.Web.Mvc;
using CentralSecurityProject.Common;
using CentralSecurityProject.Models.Security;

namespace CentralSecurityProject.Controllers.Security
{
    /// <summary>
    /// کلاس کنترلر مربوط به کاربران
    /// </summary>
    [MyRoleAuthorize(RoleName = "administrator")] // به واسطه این خصوصیت تمام متدهای داخل این کنترلر نیاز دارد که کاربر آن وارد سیستم شده باشد
    public class UserController : BaseController<Models.Security.UserModel>
    {
        /// <summary>
        /// ایجاد کلاس سازنده پیش فرض
        /// </summary>
        public UserController()
        {
            MyInitialize();
        }

        /// <summary>
        /// متد مربوط به تنظیمات کنترلر کاربر
        /// </summary>
        private void MyInitialize()
        {
            ViewBag.Title = "کاربر";
            ViewBag.AspNetUsers = new SelectList(_context.Users, "ID", "Username");
        }

        /// <summary>
        /// متد مربوط به ارسال فایل تصویری به سمت سرور
        /// </summary>
        /// <param name="instance"></param>
        public override void OnBeforePost(UserModel instance)
        {
            byte[] postedFile = new byte[instance.Photo.InputStream.Length];
            instance.Photo.InputStream.Read(postedFile, 0, postedFile.Length);

            instance.PhotoFileContent = postedFile;
            instance.PhotoFileName = instance.Photo.FileName;

            base.OnBeforePost(instance);
        }

        /// <summary>
        /// متد جهت فراخوانی اطلاعات تصویر
        /// و نمایش سمت کلاینت
        /// </summary>
        /// <param name="id">شناسه جدول</param>
        /// <returns></returns>
        public ActionResult GetUserPhoto(int id)
        {
            //byte[] photo = EntityCollection.FirstOrDefault(x => x.UserId == id).PhotoFileContent;
            //return File(photo, "img/png");

            return File(Single(id).PhotoFileContent, "img/png");
        }
    }
}