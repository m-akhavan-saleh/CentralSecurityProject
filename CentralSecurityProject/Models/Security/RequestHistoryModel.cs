using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به ثبت درخواست کاربران سیستم جامع
    /// </summary>
    [Table("tbRequestHistory", Schema = "Security")] // Default Name : RequesthistoryModels
    public class RequestHistoryModel : Security.SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد سازنده پیش فرض
        /// </summary>
        public RequestHistoryModel()
        {
        }

        #endregion

        #region Property(s)
        [Key]
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestHistoryId", Order = 0, Description = "شناسه تاریخچه درخواست")]
        [Column("RequestHistoryId", Order = 0, TypeName = "int")]
        public int RequestHistoryId { get; set; }
        public override int ID
        {
            get
            {
                return RequestHistoryId;
            }
        }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestHistoryDate", Order = 1, Description = "تاریخ ثبت تاریخجه درخواست")]
        [Column("RequestHistoryDate", Order = 1, TypeName = "datetime")]
        [System.ComponentModel.DefaultValue("getdate()")]
        public System.DateTime RequestHistoryDate { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestId", Order = 2, Description = "شماره درخواست")]
        [Column("RequestId", Order = 2, TypeName = "int")]
        public int RequestId { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDate", Order = 3, Description = "تاریخ درخواست")]
        [Column("RequestDate", Order = 3, TypeName = "datetime")]
        [System.ComponentModel.DefaultValue("getdate()")]
        public System.DateTime RequestDate { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeName", Order = 4, Description = "نوع درخواست")]
        [Column("RequestTypeId", Order = 4, TypeName = "int")]
        public int RequestTypeId { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDescription", Order = 5, Description = "توضیحات درخواست")]
        [Column("RequestDescription", Order = 5, TypeName = "nvarchar")]
        [MyMaxLength(250)]
        public string RequestDescription { get; set; }

        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusName", Order = 6, Description = "وضعیت درخواست")]
        [Column("RequestStatusId", Order = 6, TypeName = "int")]
        public int RequestStatusId { get; set; }

        [Display(Name = "کارشناس", Order = 7, Description = "کارشناس")]
        [Column("ExpertId", Order = 7, TypeName = "int")]
        public int? ExpertId { get; set; }

        [Display(Name = "تاریخ و زمان تخصیص کارشناس", Order = 8, Description = "تاریخ و زمان تخصیص کارشناس")]
        [Column("ExpertDate", Order = 8, TypeName = "datetime")]
        public System.DateTime? ExpertDate { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Response", Order = 9, Description = "پاسخ")]
        [Column("Response", Order = 9, TypeName = "nvarchar")]
        [MyMaxLength(500)]
        public string Response { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به جدول
        /// تاریخچه درخواست کاربران
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RequestHistoryModel>
        {
            public Configuration()
            {
                this.ToTable("tbRequestHistory", "Security");
            }
        }

        #endregion
    }
}