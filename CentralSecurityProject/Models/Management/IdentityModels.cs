using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CentralSecurityProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Property(s)
        public string Address { get; set; } // تعریف ستون های اطلاعاتی اضافی مورد نیاز

        #endregion

        #region Collection(s)
        public virtual System.Collections.Generic.ICollection<Models.Security.UserModel> User_List { get; set; } // جهت برقراری ارتباط یک به یک

        #endregion

        #region Method(s)
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        #endregion

        #region Configuration(s)
        /// <summary>
        /// کلاس تنظیمات مربوط به کلاس کاربران سیستم
        /// </summary>
        internal class Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<ApplicationUser>
        {
            public Configuration()
            {
                ToTable("tbUser", "AspNet");

                HasMany(m => m.User_List).
                    WithOptional(m => m.User_AspNetUser).
                    HasForeignKey(m => m.AspNetUserId).
                    WillCascadeOnDelete(false);

                HasMany(x => x.Claims).
                    WithRequired().
                    HasForeignKey(f => f.UserId);

                HasMany(x => x.Roles).
                    WithRequired().
                    HasForeignKey(f => f.UserId);

            }
        }

        #endregion
    }
}