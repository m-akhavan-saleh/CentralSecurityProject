using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به تعریف برنامه های کاربردی
    /// و زیر سیستم های موجود در سیستم جامع
    /// </summary>
    [Table("tbApplication", Schema = "Security")] // Default : ApplicationModels
    public class ApplicationModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public ApplicationModel()
        {
        }

        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        /// <param name="applicationNum">شماره برنامه</param>
        /// <param name="applicationName">عنوان برنامه</param>
        /// <param name="applicationGroupId">شناسه گروه بندی برنامه</param>
        /// <param name="filePath">مسیر فایل</param>
        /// <param name="pathExecute">مسیر برنامه</param>
        /// <param name="visibleMenu">منو قابل مشاهده</param>
        /// <param name="isWebApp">برنامه تحت وب</param>
        /// <param name="isActive">وضعیت</param>
        /// <param name="connectionString">رشته اتصال</param>
        /// <param name="projectName">عنوان پروژه</param>
        public ApplicationModel(int applicationNum, string applicationName, int applicationGroupId, string filePath, string pathExecute,
            bool visibleMenu, bool isWebApp, bool isActive, string connectionString, string projectName)
        {
            ApplicationId = 0;
            ApplicationNum = applicationNum;
            ApplicationName = applicationName;
            ApplicationGroupId = applicationGroupId;
            FilePath = filePath;
            PathExecute = pathExecute;
            VisibleMenu = visibleMenu;
            IsWebApp = isWebApp;
            IsActive = isActive;
            ConnectionString = connectionString;
            ProjectName = projectName;
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه برنامه کاربردی
        /// </summary>
        [Key]
        [Required(ErrorMessage = "{0} قید نشده است")] // {0} equal Display Attribute (Name="?")
        [Display(Name = "شناسه برنامه", Order = 0, Description = "شناسه برنامه کاربردی")]
        [Column("ApplicationId", Order = 0)]
        [DefaultValue(0)]
        public int ApplicationId { get; set; }
        public override int ID
        {
            get
            {
                return ApplicationId;
            }
        }

        /// <summary>
        /// شماره برنامه کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Range(100, 200, ErrorMessage = "{0} می بایست بین عدد {1} تا {2} تعریف شود")] // {0} equal Display Attribute and {1}{2} Range Attribute
        //[Range(int.MinValue, int.MaxValue, ErrorMessage = "Value for {0} must be between {1} and {2}.")] // use of min and max type
        [Display(Name = "شماره برنامه", Order = 1, Description = "شماره برنامه کاربردی")]
        [Column("ApplicationNum", Order = 1, TypeName = "int")]
        public int ApplicationNum { get; set; }

        /// <summary>
        /// عنوان برنامه کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "عنوان برنامه", Order = 2, Description = "عنوان برنامه کاربردی")]
        [Column("ApplicationName", Order = 2, TypeName = "nvarchar")]
        [MaxLength(50, ErrorMessage = "طول رشته {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// شناسه گروه بندی زیر سیستم ها
        /// یا همان برنامه های کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "شناسه گروه", Order = 3, Description = "عنوان گروه بندی سیستم ها")]
        [Column("ApplicationGroupId", Order = 3)]
        public int ApplicationGroupId { get; set; }

        /// <summary>
        /// مسیر فایل برنامه کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "مسیر فایل برنامه", Order = 4, Description = "مسیر فایل برنامه کاربردی")]
        [Column("FilePath", Order = 4, TypeName = "nvarchar")]
        [MaxLength(250, ErrorMessage = "طول رشته {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")]
        [RegularExpression("([^\\s]+(\\.(?i)(exe|bat))$)", ErrorMessage = "شکل {0} درست قید نشده است.")]
        public string FilePath { get; set; }

        /// <summary>
        /// مسیر فایل اجرایی برنامه کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Compare("FilePath", ErrorMessage = "{0} با مسیر فایل برنامه یکسان نمی باشد.")] // {0} equal Display Attribute (Name="?")
        [Display(Name = "مسیر فایل اجرایی", Order = 5, Description = "مسیر فایل برنامه کاربردی")]
        [Column("PathExecute", Order = 5, TypeName = "nvarchar")]
        [MaxLength(250, ErrorMessage = "طول رشته {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")]
        [RegularExpression("([^\\s]+(\\.(?i)(exe|bat))$)", ErrorMessage = "شکل {0} درست قید نشده است.")]
        public string PathExecute { get; set; }

        /// <summary>
        /// وضعیت مشاهده/عدم مشاهده منوها
        /// در برنامه های کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "وضعیت منو", Order = 6, Description = "وضعیت قابل مشاهده/عدم مشاهده منو ها در برنامه کاربردی")]
        [Column("VisibleMenu", Order = 6, TypeName = "bit")]
        public bool VisibleMenu { get; set; }

        /// <summary>
        /// برنامه از نوع برنامه های کاربردی در محیط وب
        /// یا از نوع برنامه های کاربردی در محیط ویندوز
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "برنامه صفحه گسترده", Order = 7, Description = "برنامه از نوع برنامه های کاربردی در محیط وب را مشخص می کند")]
        [Column("IsWebApp", Order = 7, TypeName = "bit")]
        public bool IsWebApp { get; set; }

        /// <summary>
        /// وضعیت فعال/غیرفعال برنامه کاربردی
        /// </summary>
        [Required(ErrorMessage = "{0} قید نشده است")]
        [Display(Name = "وضعیت", Order = 8, Description = "وضعیت فعال/غیرفعال برنامه کاربردی را مشخص می کند")]
        [Column("IsActive", Order = 8, TypeName = "bit")]
        public bool IsActive { get; set; }

        /// <summary>
        /// رشته اتصال به بانک اطلاعاتی هر یک از برنامه های کاربردی
        /// </summary>
        [Display(Name = "رشته اتصال", Order = 9, Description = "رشته اتصال به بانک اطلاعاتی برنامه کاربردی")]
        [Column("ConnectionString", Order = 9, TypeName = "nvarchar")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")] // {0} equal Display Attribute (Name="?") and {1} equal MaxLength Attribute (?)
        public string ConnectionString { get; set; }

        /// <summary>
        /// عنوان پروژه مربوط به برنامه کاربردی
        /// </summary>
        [Display(Name = "عنوان پروژه", Order = 10, Description = "عنوان پروژه برنامه کاربردی")]
        [Column("ProjectName", Order = 10, TypeName = "nvarchar")]
        [MaxLength(100, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} کراکتر تعریف شود")]
        public string ProjectName { get; set; }

        #endregion

        #region ForeignKey(s)
        /// <summary>
        /// اطلاعات گروه بندی برنامه ها/زیر سیستم ها
        /// </summary>
        [ForeignKey("ApplicationGroupId")]
        public virtual ApplicationGroupModel ApplicationGroup { get; set; } // Navigation property
        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست رابطه بین کاربر و زیر سیستم که
        /// در زیرسیستم/برنامه کاربردی فوق قرار دارند
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AppUserModel> AppUser_List { get; set; }

        /// <summary>
        /// لیست رابطه بین گروه دسترسی و زیر سیستم
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AppGroupModel> AppGroup_List { get; set; }

        /// <summary>
        /// لیست پارامترهای زیر سیستم/برنامه کاربردی
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AppParameterModel> AppParameter_List { get; set; }

        /// <summary>
        /// لیست منابع زیر سیستم/برنامه کاربردی
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AppResourceModel> AppResources { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات جدول
        /// زیر سیستم های موجود در سیستم جامع
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationModel>
        {
            public Configuration()
            {
                this.ToTable("tbApplication", "Security");

                HasMany(x => x.AppUser_List).
                    WithRequired(x => x.AppUser_ApplicationModel).
                    WillCascadeOnDelete(false);

                HasMany(x => x.AppGroup_List).
                    WithRequired(x => x.AppGroup_ApplicationModel).
                    WillCascadeOnDelete(false);

                HasMany(x => x.AppParameter_List).
                    WithRequired(x => x.AppParameter_ApplicationId).
                    WillCascadeOnDelete(false);

                HasMany(x => x.AppResources).
                    WithRequired(x => x.Application).
                    WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}