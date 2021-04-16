using System;
using System.Data.SqlClient;
using System.Data.Entity.Core;

namespace CentralSecurityProject.Common
{
    public static class MyErrorHandler
    {
        /// <summary>
        /// متد مربوط به یافتن درونی ترین خطا
        /// </summary>
        /// <param name="ex">خطا</param>
        /// <returns></returns>
        private static Exception FindInnerException(Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            return FindInnerException(ex.InnerException);
        }

        /// <summary>
        /// ترجمه خطای رخ داده شده به زبان فارسی
        /// </summary>
        /// <param name="ex">خطا</param>
        /// <returns></returns>
        public static string TranslateErrorMessage(Exception ex)
        {
            Exception innerException = FindInnerException(ex);
            string error = string.Empty;
            if (innerException is SqlException)
            {
                switch ((innerException as SqlException).Number)
                {
                    case 2601:
                        if (innerException.Message.IndexOf("UK_tbApplicationGroup_ApplicationGroupName") > 0) error = "عنوان گروه تکراری است.";
                        if (innerException.Message.IndexOf("UK_tbRole_RoleCode") > 0) error = "کد نقش تکراری است.";
                        if (innerException.Message.IndexOf("UK_tbRole_RoleName") > 0) error = "عنوان نقش تکراری است.";
                        if (innerException.Message.IndexOf("UK_tbUser_AspNetUserId") > 0) error = "کاربر تخصیص داده شده تکراری است.";
                        break;
                    case 547:
                        error = "به دلیل استفاده در سایر جداول امکان حذف آن وجود ندارد.";
                        break;
                    default:
                        error = "خطای ناشناخته ، لطفا مراتب به مدیر سیستم اطلاع داده شود.";
                        break;
                }
            }
            else
            {
                error = "خطای ناشناخته ، لطفا مراتب به مدیر سیستم اطلاع داده شود.";
            }
            return error;
        }
    }
}