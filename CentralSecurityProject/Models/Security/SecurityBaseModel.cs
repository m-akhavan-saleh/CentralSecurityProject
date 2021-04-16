using CentralSecurityProject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CentralSecurityProject.Models.Security
{
    public class SecurityBaseModel : System.Object
    {
        #region Constructor(s)
        /// <summary>
        /// ایجاد یک سازنده پیش فرض
        /// </summary>
        public SecurityBaseModel()
        {
        }

        #endregion

        #region Property(s)
        /// <summary>
        /// شناسه کاربر ثبت کننده
        /// </summary>
        [Display(Name = "کاربر ثبت کننده", Order = 50, Description = "شناسه کاربر ثبت کننده")]
        [Column("InsertUserId", Order = 50, TypeName = "nvarchar")]
        [MyMaxLength(128)]
        public string InsertUserId { get; set; }

        /// <summary>
        /// تاریخ و زمان ثبت اطلاعات
        /// </summary>
        [Display(Name = "تاریخ و زمان ثبت", Order = 51, Description = "تاریخ و زمان ثبت اطلاعات")]
        [Column("InsertDate", Order = 51, TypeName = "datetime")]
        public System.DateTime? InsertDate { get; set; }

        /// <summary>
        /// شناسه کاربر ویرایش کننده
        /// </summary>
        [Display(Name = "کاربر ویرایش کننده", Order = 52, Description = "شناسه کاربر ویرایش کننده")]
        [Column("EditUserId", Order = 52, TypeName = "nvarchar")]
        [MyMaxLength(128)]
        public string EditUserId { get; set; }

        /// <summary>
        /// تاریخ و زمان ویرایش اطلاعات
        /// </summary>
        [Display(Name = "تاریخ و زمان ویرایش", Order = 53, Description = "تاریخ و زمان ویرایش اطلاعات")]
        [Column("EditDate", Order = 53, TypeName = "datetime")]
        public System.DateTime? EditDate { get; set; }

        /// <summary>
        /// معادل همان کلید اصلی
        /// </summary>
        [NotMapped]
        [Display(Name = "شناسه", Order = 54, Description = "شناسه")]
        [Column("ID", Order = 54, TypeName = "int")]
        public virtual int ID { get; }

        /// <summary>
        /// انتخاب شده
        /// </summary>
        [NotMapped]
        [Display(Name = "انتخاب", Order = 55, Description = "انتخاب ردیف اطلاعات")]
        [Column("Selected", Order = 55, TypeName = "bit")]
        public bool Selected { get; set; }

        #endregion
    }
}