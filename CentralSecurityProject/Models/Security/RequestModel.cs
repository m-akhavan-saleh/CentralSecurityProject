using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به ثبت درخواست کاربران سیستم جامع
    /// </summary>
    [Table("tbRequest", Schema = "Security")] // Default Name : RequestModels
    public class RequestModel : Security.SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد سازنده پیش فرض
        /// </summary>
        public RequestModel()
        {
            RequestDate = System.DateTime.Now;
        }

        #endregion

        #region Property(s)
        [Key]
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestId", Order = 0, Description = "شماره درخواست")]
        [Column("RequestId", Order = 0, TypeName = "int")]
        public int RequestId { get; set; }
        public override int ID
        {
            get
            {
                return RequestId;
            }
        }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDate", Order = 1, Description = "تاریخ درخواست")]
        [Column("RequestDate", Order = 1, TypeName = "datetime")]
        [System.ComponentModel.DefaultValue("getdate()")]
        public System.DateTime RequestDate { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeName", Order = 2, Description = "نوع درخواست")]
        [Column("RequestTypeId", Order = 2, TypeName = "int")]
        public int RequestTypeId { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDescription", Order = 3, Description = "توضیحات درخواست")]
        [Column("RequestDescription", Order = 3, TypeName = "nvarchar")]
        [MyMaxLength(250)]
        public string RequestDescription { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusName", Order = 4, Description = "وضعیت درخواست")]
        [Column("RequestStatusId", Order = 4, TypeName = "int")]
        public int RequestStatusId { get; set; }

        [Display(Name = "کارشناس", Order = 5, Description = "کارشناس")]
        [Column("ExpertId", Order = 5, TypeName = "int")]
        public int? ExpertId { get; set; }

        [Display(Name = "تاریخ و زمان تخصیص کارشناس", Order = 6, Description = "تاریخ و زمان تخصیص کارشناس")]
        [Column("ExpertDate", Order = 6, TypeName = "datetime")]
        public System.DateTime? ExpertDate { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Response", Order = 3, Description = "پاسخ")]
        [Column("Response", Order = 7, TypeName = "nvarchar")]
        [MyMaxLength(500)]
        public string Response { get; set; }

        #endregion

        #region ForeignKey(s)
        /// <summary>
        /// اطلاعات انواع درخواست
        /// </summary>
        [ForeignKey("RequestTypeId")]
        public virtual RequestTypeModel RequestType { get; set; } // Navigation property

        /// <summary>
        /// اطلاعات وضعیت درخواست
        /// </summary>
        [ForeignKey("RequestStatusId")]
        public virtual RequestStatusModel RequestStatus { get; set; } // Navigation property

        /// <summary>
        /// اطلاعات کارشناسان
        /// تخصیص داده شده
        /// </summary>
        [ForeignKey("ExpertId")]
        public virtual UserModel Expert { get; set; } // Navigation property

        #endregion

        #region Collection(s)

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به جدول
        /// درخواست کاربران
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RequestModel>
        {
            public Configuration()
            {
                this.ToTable("tbRequest", "Security");
            }
        }

        #endregion
    }
}