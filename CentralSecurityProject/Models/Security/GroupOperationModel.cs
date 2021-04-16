using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط تعریف عملیات های مجاز
    /// در هر یک از گروه های دسترسی زیر سیستم ها
    /// </summary>
    [Table("tbGroupOperation", Schema = "Security")] // Default Name : GroupOperationModels
    public class GroupOperationModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public GroupOperationModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه عملیات گروه
        /// یا منابع قابل دسترسی گروه
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "شناسه عملیات گروه", Order = 0, Description = "شناسه عملیات گروه")]
        [Column("GroupOperationId", Order = 0)]
        public int GroupOperationId { get; set; }

        /// <summary>
        /// شناسه گروه دسترسی 
        /// هر یک از زیر سیستم ها
        /// </summary>
        [Required]
        [Display(Name = "شناسه گروه دسترسی", Order = 1, Description = "شناسه گروه دسترسی")]
        [Column("AppGroupId", Order = 1, TypeName = "int")]
        public int AppGroupId { get; set; }
        [ForeignKey("AppGroupId")]
        public virtual Models.Security.AppGroupModel GroupOperation_AppGroupModel { get; set; }

        /// <summary>
        /// شناسه عملیات یا منابع سیستم 
        /// هر یک از زیر سیستم ها
        /// </summary>
        [Required]
        [Display(Name = "شناسه عملیات", Order = 2, Description = "شناسه عملیات/منابع سیستم")]
        [Column("OperationId", Order = 2, TypeName = "int")]
        public int OperationId { get; set; }
        [ForeignKey("OperationId")]
        public virtual Models.Security.AppResourceModel GroupOperation_OperationModel { get; set; }

        /// <summary>
        /// دسترسی : دارد/ندارد
        /// </summary>
        [Required]
        [Display(Name = "دسترسی", Order = 3, Description = "وضعیت دسترسی : دارد/ندارد")]
        [Column("HasAccess", Order = 3, TypeName = "bit")]
        public bool HasAccess { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات جدول
        /// رابطه بین عملیات/منابع با گروه دسترسی
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<GroupOperationModel>
        {
            public Configuration()
            {
                this.ToTable("tbGroupOperation", "Security");
            }
        }

        #endregion
    }

}