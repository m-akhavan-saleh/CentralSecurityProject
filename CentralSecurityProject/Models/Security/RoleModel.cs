using System.Collections.Generic;
using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    /// <summary>
    /// کلاس مربوط به تعریف نقش
    /// </summary>
    [Table("tbRole", Schema = "Security")] // Default Name : RoleModels
    public class RoleModel : SecurityBaseModel, IValidatableObject
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public RoleModel()
        {
            this.User_List = new HashSet<UserModel>();
        }
        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه نقش
        /// </summary>
        [Key]
        [MyRequired]
        [Column("RoleId", Order = 0)]
        //[Display(Name = "شناسه نقش", Order = 0, Description = "شناسه نقش")]
        [MyDisplay("RoleId")]
        public int RoleId { get; set; }
        public override int ID
        {
            get
            {
                return RoleId;
            }
        }

        /// <summary>
        /// کد نقش
        /// </summary>
        [MyRequired]
        [Column("RoleCode", Order = 1, TypeName = "nvarchar")]
        //[Display(Name = "RoleCode", Order = 1, Description = "کد نقش")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RoleCode", Order = 0, Description = "کد نقش")]
        [MyMaxLength(5)]
        [Index("UK_tbRole_RoleCode", IsUnique = true, Order = 0)]
        public string RoleCode { get; set; }

        /// <summary>
        /// عنوان نقش
        /// </summary>
        [MyRequired]
        [Column("RoleName", Order = 2, TypeName = "nvarchar")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RoleName", Order = 2, Description = "عنوان نقش")]
        [Index("UK_tbRole_RoleName", IsUnique = true, Order = 0)]
        [MyMaxLength(100)]
        public string RoleName { get; set; }

        /// <summary>
        /// وضعیت: فعال/غیرفعال
        /// </summary>
        [MyRequired]
        [Column("IsActive", Order = 3, TypeName = "bit")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "IsActive", Order = 3, Description = "وضعیت نقش: فعال/غیرفعال")]
        public bool IsActive { get; set; }

        /// <summary>
        /// توضیحات مربوط به نقش های تعریف شده
        /// </summary>
        [Column("Description", Order = 4, TypeName = "nvarchar")]
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Description", Order = 4, Description = "توضیحات")]
        [UnusualWord(3)] // کنترل تعداد کلمات غیرمعمول بکار رفته
        public string Description { get; set; }
        #endregion

        #region Collection(s)
        /// <summary>
        /// لیست کاربران
        /// </summary>
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Users", Order = 5, Description = "لیست کاربران")]
        public virtual ICollection<UserModel> User_List { get; set; }

        #endregion

        #region Validation(s)
        /// <summary>
        /// متدی جهت بررسی اعتبار سنجی
        /// فیلدهای اطلاعاتی مربوط به جدول نقش
        /// </summary>
        /// <param name="validationContext">محتوای اعتبارسنجی</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // برای پیاده سازی می بایست مطابق مراحل زیر اقدام شود
            // First Step  : Define "IValidatableObject"
            // Second Step : Implement "Validate" Method

            if (RoleName.Contains("#")) yield return new ValidationResult("خطا به دلیل استفاده از کراکتر # در عنوان نقش.");
            if (RoleName.Contains("$")) yield return new ValidationResult("خطا به دلیل استفاده از کراکتر $ در عنوان نقش.");

            if (RoleCode.Contains("#")) yield return new ValidationResult("خطا به دلیل استفاده از کراکتر # در کد نقش.");
            if (RoleCode.Contains("$")) yield return new ValidationResult("خطا به دلیل استفاده از کراکتر $ در کد نقش.");


            //  متدهای تکرار شونده [Iterator method‌]
            // yield return value 1
            // yield return value 2
            // yield return value 3
            // ...
            // yield return value n
            // yield break equal return
        }

        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به جدول نقش
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RoleModel>
        {
            public Configuration()
            {
                this.ToTable("tbRole", "Security");
            }
        }
        #endregion
    }
}