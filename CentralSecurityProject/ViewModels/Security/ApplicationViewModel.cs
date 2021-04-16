using System.ComponentModel.DataAnnotations;

namespace CentralSecurityProject.ViewModels.Security
{
    /// <summary>
    /// کلاس ترکیبی از کلاس های
    /// برنامه ها و گروه بندی برنامه ها
    /// </summary>
    public class ApplicationViewModel
    {
        [Display(Name = "شناسه برنامه", Order = 0, Description = "شناسه برنامه")]
        public int ID { get; set; }
        [Display(Name = "شناسه برنامه", Order = 1, Description = "شناسه برنامه")]
        public int ApplicationId { get; set; }
        [Display(Name = "شماره برنامه", Order = 2, Description = "شماره برنامه")]
        public int ApplicationNum { get; set; }
        [Display(Name = "عنوان برنامه", Order = 3, Description = "عنوان برنامه")]
        public string ApplicationName { get; set; }
        [Display(Name = "وضعیت", Order = 4, Description = "وضعیت برنامه")]
        public bool IsActive { get; set; }
        [Display(Name = "گروه", Order = 5, Description = "گروه برنامه")]
        public string ApplicationGroupName { get; set; }
    }
}