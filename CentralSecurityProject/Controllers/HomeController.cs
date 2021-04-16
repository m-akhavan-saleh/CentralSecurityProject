using System.Web.Mvc;

namespace CentralSecurityProject.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "درباره ما";
            ViewBag.Message = "برنامه امنیت مرکزی که بصورت پروژه آموزشی در دوره کلاس MVC نوشته شده است.";
            ViewBag.Teacher = "سعید رجائی";
            ViewBag.TeacherDesc = "مدرس موسسه سماتک";
            ViewBag.Student = "محمد اخوان صالح";
            ViewBag.StudentDesc = "دانشجوی دوره MVC تابستان سال 1397";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void CreateDataBase()
        {
            // تعریف یک شئی از کلاس دیتا بیس کانتکست
            Models.ApplicationDbContext objDBContext = null;
            try
            {
                // ساخت شئی از نوع کلاس دیتا بیس کانتکست
                objDBContext = new Models.ApplicationDbContext();

                #region [ApplicationGroup]
                Models.Security.ApplicationGroupModel objAppGrpModel = new Models.Security.ApplicationGroupModel();
                objAppGrpModel.ApplicationGroupId = 1;
                objAppGrpModel.ApplicationGroupName = "سیستم های مدیریتی";
                objAppGrpModel.IsActive = true;

                objDBContext.ApplicationGroupModels.Add(objAppGrpModel);
                #endregion

                #region [Application]
                Models.Security.ApplicationModel objApp = new Models.Security.ApplicationModel();
                objApp.ApplicationId = 1;
                objApp.ApplicationNum = 10;
                objApp.ApplicationName = "امنیت مرکزی";
                objApp.ApplicationGroupId = 1;
                objApp.FilePath = @"c:\temp";
                objApp.PathExecute = @"c:\temp\security.exe";
                objApp.VisibleMenu = true;
                objApp.IsWebApp = false;
                objApp.IsActive = true;
                objApp.ConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CentralSecurityDB;Data Source=.";
                objApp.ProjectName = "CentralSecurityDB";

                objDBContext.ApplicationModels.Add(objApp);
                #endregion

                #region [User]
                Models.Security.UserModel objUser = new Models.Security.UserModel();
                objUser.UserId = 1;
                objUser.UserLogin = "493046";
                objUser.UserName = "M. Akhavan Saleh";
                objUser.Departement = "IT Dep.";
                objUser.Post = "Expert";
                objUser.Telephon = "2231";
                objUser.PersonalId = 10;
                objUser.IsActive = true;
                objUser.Password = "1234567";
                objUser.OldPass = "1111111";
                objUser.SetPassDate = System.DateTime.Now;
                objUser.ChangePassAtNextLogon = true;
                objUser.UserCanNotChangePass = true;
                objUser.MaxPassAge = 365;
                objUser.MinPassAge = 90;
                objUser.MinPassLength = 7;
                objUser.PassMustComplexity = false;
                objUser.EnforcePassHistory = 30;

                objDBContext.UserModels.Add(objUser);
                #endregion

                #region [AppUser]
                Models.Security.AppUserModel objAppUser = new Models.Security.AppUserModel();
                objAppUser.AppUserId = 1;
                objAppUser.UserId = 1;
                objAppUser.ApplicationId = 1;
                objAppUser.IsAdmin = false;
                objAppUser.IsActive = true;
                objAppUser.LastMachineName = "mcp-pc0635";

                objDBContext.AppUserModels.Add(objAppUser);
                #endregion

                #region [Operation]
                Models.Security.AppResourceModel objOp = new Models.Security.AppResourceModel();
                objOp.AppResourceId = 1;
                objOp.AppResourceNo = 10;
                objOp.RefAppResourceId = 1;
                objOp.ApplicationId = 1;
                objOp.ResourceName = "mnuItemBase";
                objOp.ResourceDesc = "اطلاعات پایه";
                objOp.Show = true;
                objOp.ControlName = null;

                objDBContext.AppResourceModels.Add(objOp);
                #endregion

                #region [AppGroup]
                Models.Security.AppGroupModel objAppGroup = new Models.Security.AppGroupModel();
                objAppGroup.AppGroupId = 1;
                objAppGroup.AppGroupNo = 1;
                objAppGroup.AppGroupName = "System Administrator";
                objAppGroup.ApplicationId = 1;
                objAppGroup.IsActive = true;

                objDBContext.AppGroupModels.Add(objAppGroup);
                #endregion

                #region [AppParameter]
                Models.Security.AppParameterModel objAppParam = new Models.Security.AppParameterModel();
                objAppParam.AppParameterId = 1;
                objAppParam.ApplicationId = 1;
                objAppParam.ParamString = "%1 %2";

                objDBContext.AppParameterModels.Add(objAppParam);
                #endregion

                #region [MemberOfGroup]
                Models.Security.MemberOfGroupModel objMOG = new Models.Security.MemberOfGroupModel();
                objMOG.MemberOfGroupId = 1;
                objMOG.AppUserId = 1;
                objMOG.AppGroupId = 1;

                objDBContext.MemberOfGroupModels.Add(objMOG);
                #endregion

                #region [UserOperation]
                Models.Security.UserOperationModel objUserOp = new Models.Security.UserOperationModel();
                objUserOp.UserOperationId = 1;
                objUserOp.AppUserId = 1;
                objUserOp.OperationId = 1;
                objUserOp.HasAccess = true;

                objDBContext.UserOperationModels.Add(objUserOp);
                #endregion

                objDBContext.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objDBContext != null)
                {
                    objDBContext.Dispose();
                    objDBContext = null;
                }
            }
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult RoleUnauthorized()
        {
            return View();
        }
    }
}