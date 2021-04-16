using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به گروهبندی زیر سیستم های
    /// موجود در زیر سیستم جامع
    /// </summary>
    [Table("tbApplicationGroup", Schema = "Security")] // Default Name : ApplicationGroupModels
    [System.ComponentModel.DisplayName("گروه بندی زیر سیستم ها")]
    public class ApplicationGroupModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public ApplicationGroupModel()
        {
        }

        /// <summary>
        /// ایجاد سازنده پیش فرض
        /// </summary>
        /// <param name="applicationGroupName">عنوان گروهبندی زیرسیستم ها</param>
        /// <param name="isActive">وضعیت</param>
        public ApplicationGroupModel(string applicationGroupName,bool isActive)
        {
            ApplicationGroupId = 0;
            ApplicationGroupName = applicationGroupName;
            IsActive = isActive;
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه گروه بندی سیستم ها
        /// </summary>
        [Key]
        [Required(ErrorMessage = "شناسه گروه مشخص نشده است")]
        [Column("ApplicationGroupId", Order = 0)]
        [Display(Name = "شناسه گروه", Order = 1, Description = "شناسه گروه بندی سیستم ها")]
        public int ApplicationGroupId { get; set; }

        /// <summary>
        /// عنوان گروه بندی سیستم ها
        /// </summary>
        [Column("ApplicationGroupName", Order = 1, TypeName = "nvarchar")]
        //[StringLength(50, MinimumLength = 10, ErrorMessage = "")] برای مشخص کردن طول رشته بین حداقل و حداکثری آن استفاده می شود
        [MaxLength(50, ErrorMessage = "طول رشته {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")] // {0} equal Display Attribute (Name="?") and {1} equal MaxLength Attribute (?)
        [Required(ErrorMessage = "{0} قید نشده است")] // {0} equal Display Attribute (Name="")
        [Display(Name = "عنوان گروه", Order = 1, Description = "عنوان گروه بندی سیستم ها")]
        [Index("UK_tbApplicationGroup_ApplicationGroupName", IsUnique = true, Order = 0)]
        public string ApplicationGroupName { get; set; }

        /// <summary>
        /// وضعیت فعال/غیرفعال بودن
        /// گروه بندی سیستم ها
        /// </summary>
        [Column("IsActive", Order = 2, TypeName = "bit")]
        [Display(Name = "وضعیت", Order = 2, Description = "وضعیت فعال/غیرفعال بودن گروه بندی سیستم ها")]
        [Required(ErrorMessage = "{0} قید نشده است")]
        public bool IsActive { get; set; }

        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست برنامه های کاربردی که
        /// در گروهبندی سیستم فوق قرار دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ApplicationModel> Application_List { get; set; }
        #endregion

        #region Configuration(s)
        /*
         * Fluent API vs Attribute 
         * استفاده از کلاس تنظیمات را روش فلونت ای پی ای می گویند
         * برخی از ویژگی های مربوطه را نمی توان از طریق اتریبیوت
         * برای فیلد و جدول موجود در انتی تی فریم ورک استفاده کرد
         * بهتر است که از روش فلونت ای پی ای آن هم با استفاده از
         * کلاس تنظیمات آن را انجام نمود 
         */

        /// <summary>
        /// کلاس تنظیمات جدول
        /// گروه بندی زیر سیستم ها
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationGroupModel>
        {
            public Configuration()
            {
                // Note : Attribute Is Better OR Fluent API!
                // تنظیم نام جدول و شمای جدول
                this.ToTable("tbApplicationGroup", "Security");

                // Note : Attribute Is Better OR Fluent API!
                // تظیم کلید اصلی جدول
                //HasKey(x => x.ApplicationGroupId);

                // Note : Attribute Is Better OR Fluent API!
                // تظیم خصوصیات فیلد اطلاعاتی
                //Property(x => x.ApplicationGroupId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                //Property(x => x.ApplicationGroupId).IsRequired().HasColumnOrder(0).HasColumnName("ApplicationGroupId");


                // Note : Attribute Is Better OR Fluent API!
                // تنظیم ارتباط با بین دوجدول
                HasMany(x => x.Application_List).
                    WithRequired(x => x.ApplicationGroup).
                    WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}
