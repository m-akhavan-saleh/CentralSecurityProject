using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CentralSecurityProject.ViewModels.Security
{
    /// <summary>
    /// کلاس ترکیبی از چندین کلاس که شامل کلاس های :
    /// درخواست ، نوع درخواست
    /// </summary>
    public class RequestViewModel
    {
        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestId", Order = 0, Description = "شماره درخواست")]
        public int RequestId { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ID", Order = 1, Description = "شناسه درخواست")]
        public int ID { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDate", Order = 2, Description = "تاریخ درخواست")]
        public System.DateTime RequestDate { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDate", Order = 3, Description = "تاریخ درخواست")]
        public string PersianRequestDate
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2} - {3}", pc.GetYear(RequestDate), pc.GetMonth(RequestDate), pc.GetDayOfMonth(RequestDate), RequestDate.ToShortTimeString());
            }
        }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeId", Order = 4, Description = "شناسه نوع درخواست")]
        public int RequestTypeId { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestTypeName", Order = 5, Description = "نوع درخواست")]
        public string RequestTypeName { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusId", Order = 6, Description = "شناسه وضعیت درخواست")]
        public int RequestStatusId { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestStatusName", Order = 7, Description = "وضعیت درخواست")]
        public string RequestStatusName { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestUsername", Order = 8, Description = "کاربر درخواست کننده")]
        public string RequestUsername { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestDescription", Order = 9, Description = "شرح درخواست")]
        public string RequestDescription { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ExpertId", Order = 10, Description = "کارشناس مربوطه")]
        public int ExpertId { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ExpertUsername", Order = 11, Description = "کارشناس مربوطه")]
        public string ExpertUsername { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "Response", Order = 12, Description = "پاسخ")]
        public string Response { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ExpertDate", Order = 13, Description = "تاریخ میلادی تخصیص کارشناس")]
        public System.DateTime ExpertDate { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "ExpertDate", Order = 14, Description = "تاریخ شمسی تخصیص کارشناس")]
        public string PersianExpertDate
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2} - {3}", pc.GetYear(ExpertDate), pc.GetMonth(ExpertDate), pc.GetDayOfMonth(ExpertDate), ExpertDate.ToShortTimeString());
            }
        }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestHistoryDate", Order = 15, Description = "تاریخ میلادی سوابق درخواست")]
        public System.DateTime RequestHistoryDate { get; set; }

        [Display(ResourceType = typeof(Models.Resources.Resource), Name = "RequestHistoryDate", Order = 16, Description = "تاریخ شمسی سوابق درخواست")]
        public string PersianRequestHistoryDate
        {
            get
            {
                PersianCalendar pc = new PersianCalendar();
                return string.Format("{0}/{1}/{2} - {3}", pc.GetYear(RequestHistoryDate), pc.GetMonth(RequestHistoryDate),
                    pc.GetDayOfMonth(RequestHistoryDate), RequestHistoryDate.ToShortTimeString());
            }
        }
    }
}