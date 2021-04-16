using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به تعریف اعضاء گروه
    /// </summary>
    [Table("tbMemberOfGroup", Schema = "Security")] // Default Name : MemberOfGroupModels
    public class MemberOfGroupModel : SecurityBaseModel
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public MemberOfGroupModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه اعضاء گروه دسترسی
        /// </summary>
        [Key]
        [Required]
        [Column("MemberOfGroupId", Order = 0)]
        [Display(Name = "شناسه اعضاء گروه", Order = 0, Description = "شناسه اعضاء گروه")]
        public int MemberOfGroupId { get; set; }

        /// <summary>
        /// شناسه کاربر تعریف شده 
        /// در برنامه کاربردی/زیر سیستم
        /// </summary>
        [Required]
        [Column("AppUserId", Order = 1, TypeName = "int")]
        [Display(Name = "کاربر زیر سیستم", Order = 1, Description = "شناسه کاربر زیر سیستم")]
        public int AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public virtual Models.Security.AppUserModel MemberOfGroup_AppUserId { get; set; }

        /// <summary>
        /// شناسه گروه دسترسی
        /// برنامه های کاربردی
        /// </summary>
        [Required]
        [Column("AppGroupId", Order = 2, TypeName = "int")]
        [Display(Name = "گروه دسترسی زیر سیستم", Order = 2, Description = "شناسه گروه دسترسی زیرسیستم")]
        public int AppGroupId { get; set; }
        [ForeignKey("AppGroupId")]
        public virtual Models.Security.AppGroupModel MemberOfGroup_AppGroupId { get; set; }

        #endregion
    }

    public class MembrOfGroupConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<MemberOfGroupModel>
    {
        public MembrOfGroupConfiguration()
        {
            this.ToTable("tbMemberOfGroup", "Security");
        }
    }
}