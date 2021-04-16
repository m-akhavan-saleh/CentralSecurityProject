namespace CentralSecurityProject.Models
{
    /// <summary>
    /// کلاس مربوط به تنظیمات اولیه بانک اطلاعاتی
    /// </summary>
    public class DataBaseContextInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        /// <summary>
        /// سازنده پیش فرض
        /// </summary>
        public DataBaseContextInitializer()
        {

        }

        /// <summary>
        /// این تابع زمانی اجرا می شود که بانک اطلاعاتی می خواهد ایجاد شود
        /// در صورتیکه بانک اطلاعاتی قبلا ایجاد شده باشد دیگر این متد اجرا نمی شود
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
            try
            {
                context.Database.ExecuteSqlCommand("IF EXISTS (SELECT * FROM sys.indexes WHERE Name = 'UK_tbUser_AspNetUserId') " +
                        "DROP INDEX [UK_tbUser_AspNetUserId] ON [Security].[tbUser]");
                context.Database.ExecuteSqlCommand("CREATE UNIQUE NONCLUSTERED INDEX [UK_tbUser_AspNetUserId] ON [Security].[tbUser]([AspNetUserId] ASC) " +
                        "WHERE([AspNetUserId] IS NOT NULL) " +
                        "WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)");

                // مقدار دهی اولیه برای جدول اطلاعاتی گروه بندی برنامه ها
                context.UserModels.Add(new Security.UserModel("493046", "محمد اخوان صالح", "نرم افزار", "رئیس اداره", true, "123", true, true, "2231"));
                context.UserModels.Add(new Security.UserModel("493040", "محمدرضا یاری", "سخت افزار", "رئیس اداره", true, "123", true, true, "2248"));
                context.UserModels.Add(new Security.UserModel("493032", "احمد کشت پور", "شبکه", "رئیس اداره", true, "123", true, true, "2581"));
                context.UserModels.Add(new Security.UserModel("493032", "احمد بیگی", "فرآیند", "رئیس اداره", true, "123", true, true, "2130"));
                context.SaveChanges();

                // مقدار دهی اولیه برای جدول اطلاعاتی گروه بندی برنامه ها
                context.ApplicationGroupModels.Add(new Security.ApplicationGroupModel("برنامه ریزی", true));
                context.ApplicationGroupModels.Add(new Security.ApplicationGroupModel("منابع انسانی", false));
                context.ApplicationGroupModels.Add(new Security.ApplicationGroupModel("مهندسی", true));
                context.ApplicationGroupModels.Add(new Security.ApplicationGroupModel("پشتیبانی", true));
                context.SaveChanges();

                // مقداردهی اولیه برای جدول اطلاعاتی برنامه ها/زیر سیستم ها
                context.ApplicationModels.Add(new Security.ApplicationModel(110, "برنامه ریزی مواد", 1, @"\\srv-application\Project.exe", @"\\srv-application\Project.exe", true, false, true, "connection1", "MaterialProject"));
                context.ApplicationModels.Add(new Security.ApplicationModel(112, "برنامه ریزی تولید", 1, @"\\srv-application\Project.exe", @"\\srv-application\Project.exe", true, false, true, "connection2", "PlanningProject"));
                context.ApplicationModels.Add(new Security.ApplicationModel(130, "مهندسی", 3, @"\\srv-application\Project.exe", @"\\srv-application\Project.exe", true, false, true, "connection3", "EngineerProject"));
                context.ApplicationModels.Add(new Security.ApplicationModel(132, "طرح بسته بندی", 3, @"\\srv-application\Project.exe", @"\\srv-application\Project.exe", true, false, true, "connection4", "PackingProject"));
                context.SaveChanges();

                // مقدار دهی اولیه برای جداول اطلاعاتی گروه های دسترسی برنامه ها
                context.AppGroupModels.Add(new Security.AppGroupModel(100, "مدیران سیستم", 1, true));
                context.AppGroupModels.Add(new Security.AppGroupModel(110, "مشاهدات و گزارشات", 1, true));
                context.AppGroupModels.Add(new Security.AppGroupModel(120, "دسترسی عمومی", 1, false));
                context.AppGroupModels.Add(new Security.AppGroupModel(200, "مدیران سیستم", 3, true));
                context.AppGroupModels.Add(new Security.AppGroupModel(210, "مشاهدات و گزارشات", 3, true));
                context.AppGroupModels.Add(new Security.AppGroupModel(220, "دسترسی عمومی", 3, false));
                context.SaveChanges();

                // مقدار دهی اولیه برای جدول اطلاعاتی نوع درخواست کاربر
                context.RequestTypeModels.Add(new Security.RequestTypeModel("درخواست تغییر کلمه عبور", false));
                context.RequestTypeModels.Add(new Security.RequestTypeModel("درخواست دسترسی", true));
                context.RequestTypeModels.Add(new Security.RequestTypeModel("درخواست حذف دسترسی", true));
                context.SaveChanges();

                // مقدار دهی اولیه برای جدول اطلاعاتی وضعیت درخواست کاربر
                context.RequestStatusModels.Add(new Security.RequestStatusModel("تنظیم", false));
                context.RequestStatusModels.Add(new Security.RequestStatusModel("تائید", true));
                context.RequestStatusModels.Add(new Security.RequestStatusModel("رد", true));
                context.RequestStatusModels.Add(new Security.RequestStatusModel("تخصیص کارشناس", true));
                context.RequestStatusModels.Add(new Security.RequestStatusModel("پاسخ کارشناس", true));
                context.SaveChanges();

                /*
                 *  تعریف این بخش در متد اجرای برنامه هم می توان پیاده سازی نمود
                // مقدار دهی اولیه برای جدول مربوط به نقش های کاربر سیستم
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.Administrator)));
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.Support)));
                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole(Common.MyStringEnum.GetStringValue(Common.Role.User)));
                context.SaveChanges();
                */

                context.Database.ExecuteSqlCommand("CREATE TRIGGER [Security].[Request_InsUpd] ON [Security].[tbRequest] " +
                        "FOR INSERT,UPDATE AS " +
                        "INSERT INTO[Security].[tbRequestHistory]([RequestHistoryDate],[RequestId],[RequestDate],[RequestTypeId],[RequestDescription]," +
                        "[RequestStatusId],[ExpertId],[ExpertDate],[InsertUserId],[InsertDate],[EditUserId],[EditDate],[Response]) " +
                        "SELECT GETDATE(),[RequestId],[RequestDate],[RequestTypeId],[RequestDescription]," +
                        "[RequestStatusId],[ExpertId],[ExpertDate],[InsertUserId],[InsertDate],[EditUserId],[EditDate],[Response] FROM INSERTED");

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                // بررسی خطاهای مربوط به ثبت اطلاعات
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                    }
                }
            }
        }
    }
}