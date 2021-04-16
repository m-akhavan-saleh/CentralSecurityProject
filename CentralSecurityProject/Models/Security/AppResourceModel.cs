using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به منابع زیر سیستم های
    /// موجود در زیر سیستم جامع
    /// </summary>
    [Table("tbAppResource", Schema = "Security")] // Default Name : AppResourceModels
    public class AppResourceModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public AppResourceModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه منبع زیر سیستم
        /// هر یک از زیر سیستم های 
        /// سیستم جامع
        /// </summary>
        [Key]
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "AppResourceId", Order = 0, Description = "شناسه منبع زیر سیستم")]
        [Column("AppResourceId", Order = 0)]
        public int AppResourceId { get; set; }
        public override int ID
        {
            get
            {
                return AppResourceId;
            }
        }

        /// <summary>
        /// شماره منبع
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "AppResourceNo", Order = 1, Description = "شماره منبع")]
        [Column("AppResourceNo", Order = 1)]
        public int AppResourceNo { get; set; }

        /// <summary>
        /// شناسه سیستم
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ApplicationId", Order = 2, Description = "شناسه سیستم")]
        [Column("ApplicationId", Order = 2, TypeName = "int")]
        public int ApplicationId { get; set; }

        /// <summary>
        /// شناسه منبع پدر
        /// </summary>
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RefAppResourceId", Order = 3, Description = "شناسه منبع پدر")]
        [Column("RefAppResourceId", Order = 3, TypeName = "int")]
        public int? RefAppResourceId { get; set; }

        /// <summary>
        /// منبع
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ResourceName", Order = 4, Description = "عنوان منبع")]
        [Column("ResourceName", Order = 4, TypeName = "nvarchar")]
        [MyMaxLength(100)]
        public string ResourceName { get; set; }

        /// <summary>
        /// عنوان منبع
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ResourceDesc", Order = 5, Description = "عنوان فارسی منبع")]
        [Column("ResourceDesc", Order = 5, TypeName = "nvarchar")]
        [MyMaxLength(100)]
        public string ResourceDesc { get; set; }

        /// <summary>
        /// قابل نمایش بودن
        /// منبع سیستم
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Show", Order = 6, Description = "قابل نمایش هست / نیست")]
        [Column("Show", Order = 6, TypeName = "bit")]
        public bool Show { get; set; }

        /// <summary>
        /// عنوان کنترل
        /// </summary>
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ControlName", Order = 7, Description = "عنوان کنترل")]
        [Column("ControlName", Order = 7, TypeName = "nvarchar")]
        [MyMaxLength(100)]
        public string ControlName { get; set; }
        #endregion

        #region ForeignKey(s)
        /// <summary>
        /// اطلاعات منبع زیر سیستم 
        /// </summary>
        [ForeignKey("RefAppResourceId")]
        public virtual AppResourceModel AppResource { get; set; }

        /// <summary>
        /// اطلاعات برنامه/زیر سیستم
        /// </summary>
        [ForeignKey("ApplicationId")]
        public virtual Models.Security.ApplicationModel Application { get; set; }

        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست عملیات/منابع
        /// قابل دسترسی برای گروه های دسترسی
        /// </summary>
        public virtual System.Collections.Generic.ICollection<GroupOperationModel> GroupOperations { get; set; }

        /// <summary>
        /// لیست کاربران سیستم که
        /// به منابع زیر سیستم موجود دسترسی دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserOperationModel> UserOperations { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات جدول
        /// عملیات سیستم
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppResourceModel>
        {
            public Configuration()
            {
                this.ToTable("tbAppResource", "Security");

                HasMany(x => x.GroupOperations).
                    WithRequired(x => x.GroupOperation_OperationModel).
                    WillCascadeOnDelete(false);

                HasMany(x => x.UserOperations).
                    WithRequired(x => x.UserOperation_OperationId).
                    WillCascadeOnDelete(false);

                HasOptional(x => x.AppResource).
                    WithMany().
                    HasForeignKey(x => x.RefAppResourceId).
                    WillCascadeOnDelete(false); // do delete children when parent is deleted;
            }
        }

        #endregion
    }
}