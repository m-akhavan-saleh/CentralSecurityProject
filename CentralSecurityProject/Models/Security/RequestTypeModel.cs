using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به نوع درخواست کاربر از مدیر سیستم
    /// </summary>
    [Table("tbRequestType", Schema = "Security")] // Default Name : RequestTypeModels
    public class RequestTypeModel : Security.SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        public RequestTypeModel()
        {

        }

        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        /// <param name="requestTypeName">نوع درخواست</param>
        /// <param name="isActive">وضعیت</param>
        public RequestTypeModel(string requestTypeName,bool isActive)
        {
            RequestTypeId = 0;
            RequestTypeName = requestTypeName;
            IsActive = isActive;
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه نوع درخواست
        /// </summary>
        [Key]
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeId", Order = 0, Description = "شناسه نوع درخواست")]
        [Column("RequestTypeId", Order = 0, TypeName = "int")]
        public int RequestTypeId { get; set; }
        public override int ID
        {
            get
            {
                return RequestTypeId;
            }
        }

        /// <summary>
        /// نوع درخواست
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeName", Order = 1, Description = "نوع درخواست")]
        [Column("RequestTypeName", Order = 1, TypeName = "nvarchar")]
        [MyMaxLength(60)]
        public string RequestTypeName { get; set; }

        /// <summary>
        /// وضعیت فعال/غیر فعال
        /// نوع درخواست کاربر
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "IsActive", Order = 2, Description = "وضعیت فعال/غیر فعال")]
        [Column("IsActive", Order = 2, TypeName = "bit")]
        public bool IsActive { get; set; }

        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست درخواست ها
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RequestModel> Request_List { get; set; }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به جدول
        /// نوع درخواست کاربر
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RequestTypeModel>
        {
            public Configuration()
            {
                this.ToTable("tbRequestType", "Security");

                HasMany(x => x.Request_List).
                  WithRequired(x => x.RequestType).
                  WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}