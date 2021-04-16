using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به دسترسی کاربران
    /// در هریک از زیر سیستم جامع
    /// </summary>
    [Table("tbUserOperation", Schema = "Security")] // Default Name : UserOperationModels
    public class UserOperationModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public UserOperationModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه دسترسی کاربر
        /// به هریک از منابع سیستم
        /// </summary>
        [Key]
        [Required]
        [Column("UserOperationId", Order = 0)]
        [Display(Name = "شناسه دسترسی کاربر", Order = 0, Description = "شناسه دسترسی کاربر به هریک از منابع زیر سیستم")]
        public int UserOperationId { get; set; }

        /// <summary>
        /// شناسه کاربر سیستم
        /// </summary>
        [Required]
        [Column("AppUserId", Order = 1, TypeName = "int")]
        [Display(Name = "کاربر زیرسیستم", Order = 1, Description = "شناسه کاربر زیرسیستم")]
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual Models.Security.AppUserModel UserOperation_AppUserId { get; set; }

        /// <summary>
        /// شناسه منابع زیر سیستم
        /// </summary>
        [Required]
        [Column("OperationId", Order = 2, TypeName = "int")]
        [Display(Name = "منابع زیر سیستم", Order = 2, Description = "شناسه منابع زیر سیستم")]
        public int OperationId { get; set; }
        [ForeignKey("OperationId")]
        public virtual Models.Security.AppResourceModel UserOperation_OperationId { get; set; }

        /// <summary>
        /// دسترسی : دارد/ندارد
        /// </summary>
        [Required]
        [Column("HasAccess", Order = 3, TypeName = "bit")]
        [Display(Name = "دسترسی", Order = 3, Description = "دسترسی: دارد/ندارد")]
        public bool HasAccess { get; set; }

        #endregion
    }

    /// <summary>
    /// جدول تنظیمات مربوط به عملیات
    /// مجاز کاربر که دسترسی آن در سیستم داده شده است
    /// </summary>
    public class UserOperationConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<UserOperationModel>
    {
        public UserOperationConfiguration()
        {
            this.ToTable("tbUserOperation", "Security");
        }
    }
}
