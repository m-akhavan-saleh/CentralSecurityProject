using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به وضعیت درخواست کاربر
    /// </summary>
    [Table("tbRequestStatus", Schema = "Security")] // Default Name : RequestStatusModels
    public class RequestStatusModel : Security.SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        public RequestStatusModel()
        {

        }

        /// <summary>
        /// کلاس سازنده پیش فرض
        /// </summary>
        /// <param name="RequestStatusName">وضعیت درخواست</param>
        /// <param name="isActive">وضعیت</param>
        public RequestStatusModel(string requestStatusName,bool isActive)
        {
            RequestStatusId = 0;
            RequestStatusName = requestStatusName;
            IsActive = isActive;
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه وضعیت درخواست
        /// </summary>
        [Key]
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusId", Order = 0, Description = "شناسه وضعیت درخواست")]
        [Column("RequestStatusId", Order = 0, TypeName = "int")]
        public int RequestStatusId { get; set; }
        public override int ID
        {
            get
            {
                return RequestStatusId;
            }
        }

        /// <summary>
        /// وضعیت درخواست
        /// </summary>
        [MyRequired]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusName", Order = 1, Description = "وضعیت درخواست")]
        [Column("RequestStatusName", Order = 1, TypeName = "nvarchar")]
        [MyMaxLength(60)]
        public string RequestStatusName { get; set; }

        /// <summary>
        /// وضعیت فعال/غیر فعال
        /// وضعیت درخواست کاربر
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
        /// وضعیت درخواست کاربر
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RequestStatusModel>
        {
            public Configuration()
            {
                this.ToTable("tbRequestStatus", "Security");

                HasMany(x => x.Request_List).
                 WithRequired(x => x.RequestStatus).
                 WillCascadeOnDelete(false);
            }
        }

        #endregion
    }
}