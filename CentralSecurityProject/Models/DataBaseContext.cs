namespace CentralSecurityProject.Models
{
    /// <summary>
    /// کلاس مربوط به دی بی کانتکس که از فایل مربوط به
    /// آی دنتیتی مدل برداشته شده است.
    /// --------------------------------------------------
    /// قدم اول : پارشیال کردن دی بی کانتکس ها می باشد
    /// </summary>
    public partial class ApplicationDbContext : Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DataBaseContext", throwIfV1Schema: false)
        {
            // این تنظیمات جهت انجام عملیات لیزی لودینگ استفاده می شود
            //Configuration.LazyLoadingEnabled = false; // Default : True
        }

        public static ApplicationDbContext Create() // Singletone Pattern
        {
            return new ApplicationDbContext();
        }
    }

    /// <summary>
    /// نام این کلاس هرچه می تواند باشد و لی حتما بایست از کلاس دی بی کانتکست ارث بری شده باشد
    /// </summary>
    public partial class ApplicationDbContext //: System.Data.Entity.DbContext
    {
        #region Constructor(s)
        /// <summary>
        /// سازنده پیش فرض
        /// در فایل تنظیمات می بایست عنوان کلاس دیتا بیس کانتکست
        /// را به عنوان رشته اتصال به بانک اطلاعاتی معرفی نمود
        /// </summary>
        //public DataBaseContext() : base("DataBaseContext")
        //{
        //}

        #endregion

        #region Property(s)
        /// <summary>
        /// ساخت یک کالکشن از جنس گروه بندی سیستم
        /// جنس این کالکشن از جنس دی بی ست است ولی می توان از هر کالکشن دیگری استفاده نمود
        /// سایر کاکلشن های مانند : Table , List , IEnumerable , Dictionary
        /// </summary>
        public System.Data.Entity.DbSet<Security.ApplicationGroupModel> ApplicationGroupModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس برنامه های کاربردی
        /// </summary>
        public System.Data.Entity.DbSet<Security.ApplicationModel> ApplicationModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس کاربران سیستم
        /// </summary>
        public System.Data.Entity.DbSet<Security.UserModel> UserModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس عملیات سیستم
        /// </summary>
        public System.Data.Entity.DbSet<Security.AppResourceModel> AppResourceModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس رابطه بین کاربر و زیر سیستم/برنامه کاربردی
        /// </summary>
        public System.Data.Entity.DbSet<Security.AppUserModel> AppUserModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس گروه دسترسی های زیر سیستم
        /// </summary>
        public System.Data.Entity.DbSet<Security.AppGroupModel> AppGroupModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس منابع/عملیات های قابل دسترسی هریک از گروه ها
        /// </summary>
        public System.Data.Entity.DbSet<Security.GroupOperationModel> GroupOperationModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس پارامترهای ورودی برنامه کاربردی/زیر سیستم
        /// </summary>
        public System.Data.Entity.DbSet<Security.AppParameterModel> AppParameterModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس اعضاء گروه دسترسی
        /// </summary>
        public System.Data.Entity.DbSet<Security.MemberOfGroupModel> MemberOfGroupModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس دسترسی کاربر به منابع زیرسیستم
        /// </summary>
        public System.Data.Entity.DbSet<Security.UserOperationModel> UserOperationModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از جنس نقش
        /// </summary>
        public System.Data.Entity.DbSet<Security.RoleModel> RoleModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از نوع درخواست
        /// </summary>
        public System.Data.Entity.DbSet<Security.RequestTypeModel> RequestTypeModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از وضعیت درخواست
        /// </summary>
        public System.Data.Entity.DbSet<Security.RequestStatusModel> RequestStatusModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از درخواست
        /// </summary>
        public System.Data.Entity.DbSet<Security.RequestModel> RequestModels { get; set; }

        /// <summary>
        /// ساخت یک مجموعه از تاریخچه درخواست
        /// </summary>
        public System.Data.Entity.DbSet<Security.RequestHistoryModel> RequestHistoryModels { get; set; }

        #endregion

        #region Configuration(s)
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تنظیمات مدل های مربوط به مایکروسافت
            //modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>().ToTable("tbUser", "AspNet");
            modelBuilder.Configurations.Add(new Models.ApplicationUser.Configuration());
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>().ToTable("tbRole", "AspNet");
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>().HasMany(x => x.Users)
                .WithRequired().HasForeignKey(m => m.RoleId);
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole>().ToTable("tbUserRole", "AspNet");
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim>().ToTable("tbUserClaim", "AspNet");
            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin>().ToTable("tbUserLogin", "AspNet");

            // تنظیمات مدل های مربوط به پروژه
            modelBuilder.Configurations.Add(new Security.ApplicationGroupModel.Configuration()); // Use Internal Class
            modelBuilder.Configurations.Add(new Security.ApplicationModel.Configuration());
            modelBuilder.Configurations.Add(new Security.AppParameterModel.Configuration());
            modelBuilder.Configurations.Add(new Security.UserModel.Configuration());
            modelBuilder.Configurations.Add(new Security.AppUserModel.Configuration());
            modelBuilder.Configurations.Add(new Security.AppGroupModel.Configuration());
            modelBuilder.Configurations.Add(new Security.RoleModel.Configuration());
            modelBuilder.Configurations.Add(new Security.AppResourceModel.Configuration());
            modelBuilder.Configurations.Add(new Security.GroupOperationModel.Configuration());
            modelBuilder.Configurations.Add(new Security.MembrOfGroupConfiguration());
            modelBuilder.Configurations.Add(new Security.UserOperationConfiguration());
            modelBuilder.Configurations.Add(new Security.RequestTypeModel.Configuration());
            modelBuilder.Configurations.Add(new Security.RequestStatusModel.Configuration());
            modelBuilder.Configurations.Add(new Security.RequestModel.Configuration());
            modelBuilder.Configurations.Add(new Security.RequestHistoryModel.Configuration());

            // Configure a Many-to-Many Relationship using Fluent API:
            // تنظیمات مربوط به تعریف رابطه چند به چند با استفاده از فلونت ای پی آی
            modelBuilder.Entity<Models.Security.UserModel>()
                .HasMany<Models.Security.RoleModel>(x => x.Role_List)
                .WithMany(x => x.User_List)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("tbUserRole", "Security");
                });
        }

        #endregion
    }
}