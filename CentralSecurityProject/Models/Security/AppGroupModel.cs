using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط تعریف گروه بندی های
    /// دسترسی در هر یک از زیر سیستم ها
    /// </summary>
    [Table("tbAppGroup", Schema = "Security")] // Default Name : AppGroupModels
    public class AppGroupModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public AppGroupModel()
        {
        }

        /// <summary>
        /// ایجاد سازنده پیش فرض
        /// </summary>
        /// <param name="appGroupNo">شماره گروه دسترسی</param>
        /// <param name="appGroupName">عنوان گروه دسترسی</param>
        /// <param name="applicationId">شناسه برنامه</param>
        /// <param name="isActive">وضعیت</param>
        public AppGroupModel(int appGroupNo, string appGroupName, int applicationId,bool isActive)
        {
            AppGroupId = 0;
            AppGroupNo = appGroupNo;
            AppGroupName = appGroupName;
            ApplicationId = applicationId;
            IsActive = isActive;
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه گروه دسترسی
        /// </summary>
        [Key]
        [MyRequired]
        [Column("AppGroupId", Order = 0)]
        [Display(Name = "شناسه گروه", Order = 0, Description = "شناسه گروه دسترسی")]
        public int AppGroupId { get; set; }
        public override int ID
        {
            get
            {
                return AppGroupId;
            }
        }

        /// <summary>
        /// شماره گروه دسترسی
        /// </summary>
        [MyRequired]
        [Column("AppGroupNo", Order = 1, TypeName = "int")]
        [Display(Name = "شماره گروه", Order = 1, Description = "شماره گروه دسترسی")]
        public int AppGroupNo { get; set; }

        /// <summary>
        /// عنوان گروه دسترسی
        /// </summary>
        [MyRequired]
        [Column("AppGroupName", Order = 2, TypeName = "nvarchar")]
        [Display(Name = "عنوان گروه دسترسی", Order = 2, Description = "عنوان گروه دسترسی")]
        [MyMaxLength(50)]
        public string AppGroupName { get; set; }

        /// <summary>
        /// شناسه برنامه کاربردی/زیر سیستم
        /// </summary>
        [MyRequired]
        [Column("ApplicationId", Order = 3, TypeName = "int")]
        [Display(Name = "برنامه کاربردی", Order = 3, Description = "شناسه برنامه کاربردی/زیر سیستم")]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Models.Security.ApplicationModel AppGroup_ApplicationModel { get; set; }

        /// <summary>
        /// شناسه وضعیت: فعال/غیر فعال
        /// گروه دسترسی
        /// </summary>
        [MyRequired]
        [Column("IsActive", Order = 4, TypeName = "bit")]
        [Display(Name = "وضعیت", Order = 4, Description = "وضعیت: فعال/غیرفعال")]
        public bool IsActive { get; set; }

        /// <summary>
        /// فیلد تعداد اعضاء گروه
        /// این فیلد محاسباتی می باشد
        /// و در بانک اطلاعاتی ستونی برای آن ساخته نمی شود
        /// </summary>
        [NotMapped]
        public int Count { get; set; }
        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست عملیات/منابع
        /// قابل دسترسی برای گروه های دسترسی
        /// </summary>
        public virtual System.Collections.Generic.ICollection<GroupOperationModel> GroupOperation_List { get; set; }

        /// <summary>
        /// لیست کاربران سیستم که
        /// در گروه دسترسی فوق قرار دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MemberOfGroupModel> MembrOfGroup_List { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات جدول
        /// گروه دسترسی ها
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppGroupModel>
        {
            public Configuration()
            {
                this.ToTable("tbAppGroup", "Security");

                HasMany(x => x.GroupOperation_List).
                    WithRequired(x => x.GroupOperation_AppGroupModel).
                    WillCascadeOnDelete(false);

                HasMany(x => x.MembrOfGroup_List).
                    WithRequired(x => x.MemberOfGroup_AppGroupId).
                    WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}