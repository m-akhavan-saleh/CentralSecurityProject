namespace CentralSecurityProject.Common
{
    /// <summary>
    /// ایجاد کلاس تولید تگ به روش سنتی
    /// با استفاده از یک کلاس استاتیک
    /// </summary>
    public static class TraditionalHtmlHelper
    {
        /// <summary>
        /// متد مربوط به ایجاد یک لیبل به همراه تکس باکس ورودی
        /// </summary>
        /// <param name="id">شناسه تگ</param>
        /// <param name="caption">عنوان تگ</param>
        /// <returns></returns>
        public static string Label(string id, string caption)
        {
            string result = string.Empty;
            result = string.Format("<div class='form-group'><label for='{0}'>{1} :</label><input type='text' class='form-control' id='{0}'></div>", id, caption);
            return result;
        }
    }
}