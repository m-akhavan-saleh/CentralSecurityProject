namespace CentralSecurityProject.Common
{
    /// <summary>
    /// انواع مختلف اکشن لینک
    /// </summary>
    public enum ActionLinkType
    {
        /// <summary>
        /// هیچکدام
        /// </summary>
        None,
        /// <summary>
        /// ایجاد
        /// </summary>
        Create,
        /// <summary>
        /// ویرایش
        /// </summary>
        Edit,
        /// <summary>
        /// حذف
        /// </summary>
        Delete,
        /// <summary>
        /// جزئیات
        /// </summary>
        Details,
        /// <summary>
        /// انتخاب شده
        /// </summary>
        Selected,
        /// <summary>
        /// انتخاب نشده
        /// </summary>
        Unselected,
        /// <summary>
        /// تائید
        /// </summary>
        Approve,
        /// <summary>
        /// لغو
        /// </summary>
        Cancel,
        /// <summary>
        /// ارجاع
        /// </summary>
        Referral
    }

    /// <summary>
    /// انواع نقش های کاربران سیستم
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// مدیر سیستم
        /// </summary>
        [MyStringValue("administrator")]
        Administrator,
        /// <summary>
        /// پشتیبان سیستم
        /// </summary>
        [MyStringValue("support")]
        Support,
        /// <summary>
        /// کاربر سیستم
        /// </summary>
        [MyStringValue("user")]
        User
    }
}