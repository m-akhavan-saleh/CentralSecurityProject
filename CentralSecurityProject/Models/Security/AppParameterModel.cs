using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به تعریف پارامتر های 
    /// برنامه کاربردی/زیر سیستم
    /// </summary>
    [Table("tbAppParameter", Schema = "Security")] // Default Name : AppParameterModel
    public class AppParameterModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public AppParameterModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه پارامتر ورودی زیر سیستم
        /// </summary>
        [Key]
        [MyRequired]
        [Column("AppParameterId", Order = 0)]
        [Display(Name = "شناسه پارامتر زیر سیستم", Order = 0, Description = "شناسه پارامتر ورودی زیر سیستم")]
        public int AppParameterId { get; set; }
        public override int ID
        {
            get
            {
                return AppParameterId;
            }
        }

        /// <summary>
        /// شناسه زیر سیستم/برنامه کاربردی
        /// </summary>
        [MyRequired]
        [Column("ApplicationId", Order = 1, TypeName = "int")]
        [Display(Name = "برنامه کاربردی", Order = 1, Description = "برنامه کاربردی/زیر سیستم")]
        public int ApplicationId { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Models.Security.ApplicationModel AppParameter_ApplicationId { get; set; }

        /// <summary>
        /// پارامتر ورودی زیرسیستم
        /// </summary>
        [MyRequired]
        [Column("ParamString", Order = 2, TypeName = "nvarchar")]
        [Display(Name = "پارامتر ورودی", Order = 2, Description = "رشته پارامتر ورودی برنامه کاربردی/زیر سیستم")]
        [MyMaxLength(50)]
        public string ParamString { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به جدول پارامترهای برنامه
        /// </summary>
        public class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<AppParameterModel>
        {
            public Configuration()
            {
                this.ToTable("tbAppParameter", "Security");
            }
        }

        #endregion
    }
}