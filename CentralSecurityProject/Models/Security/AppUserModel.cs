using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به تعیین رابطه
    /// بین کاربران و زیر سیستم های موجود
    /// </summary>
    [Table("tbAppUser", Schema = "Security")] // Default Name : AppUserModels
    public class AppUserModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public AppUserModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه رابطه بین
        /// کاربر و زیر سیستم
        /// </summary>
        [Key]
        [MyRequired]
        [Column("AppUserId", Order = 0)]
        [Display(Name = "شناسه", Order = 0, Description = "شناسه رابطه بین کاربر و زیر سیستم موجود")]
        public int AppUserId { get; set; }
        public override int ID
        {
            get
            {
                return AppUserId;
            }
        }

        /// <summary>
        /// شناسه کاربر
        /// </summary>
        [MyRequired]
        [Display(Name = "کاربر", Order = 1, Description = "شناسه کاربر ورودی")]
        [Column("UserId", Order = 1, TypeName = "int")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Models.Security.UserModel User { get; set; }

        /// <summary>
        /// شناسه برنامه کاربردی
        /// </summary>
        [MyRequired]
        [Display(Name = "برنامه کاربردی", Order = 2, Description = "شناسه برنامه کاربردی/زیر سیستم")]
        [Column("ApplicationId", Order = 2, TypeName = "int")]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Models.Security.ApplicationModel AppUser_ApplicationModel { get; set; }

        /// <summary>
        /// وضعیت مدیر زیر سیستم : هست/نیست
        /// </summary>
        [MyRequired]
        [Display(Name = "مدیر سیستم", Order = 3, Description = "مدیر برنامه کاربردی/زیر سیستم")]
        [Column("IsAdmin", Order = 3, TypeName = "bit")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// وضعیت : فعال/غیر فعال
        /// </summary>
        [MyRequired]
        [Display(Name = "وضعیت", Order = 4, Description = "وضعیت: فعال/غیرفعال")]
        [Column("IsActive", Order = 4, TypeName = "bit")]
        public bool IsActive { get; set; }

        /// <summary>
        /// آخرین ماشینی که از طریق آن
        /// وارد زیر سیستم مورد نظر شده است
        /// </summary>
        [MyRequired]
        [Display(Name = "آخرین ماشین", Order = 5, Description = "نمایش آخرین ماشینی که کاربر از طریق آن وارد سیستم شده است")]
        [Column("LastMachineName", Order = 5, TypeName = "nvarchar")]
        [MyMaxLength(50)]
        public string LastMachineName { get; set; }
        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست کاربران سیستم که
        /// در گروه دسترسی فوق قرار دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MemberOfGroupModel> MembrOfGroup_List { get; set; }

        /// <summary>
        /// لیست کاربران سیستم که
        /// به منابع زیر سیستم موجود دسترسی دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserOperationModel> UserOperation_List { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات جدول
        /// رابطه بین کاربر و زیر سیستم
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppUserModel>
        {
            public Configuration()
            {
                this.ToTable("tbAppUser", "Security");

                HasMany(x => x.MembrOfGroup_List).
                    WithRequired(x => x.MemberOfGroup_AppUserId).
                    WillCascadeOnDelete(false);

                HasMany(x => x.UserOperation_List).
                    WithRequired(x => x.UserOperation_AppUserId).
                    WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}