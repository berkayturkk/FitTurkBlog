using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Writer
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterAboutOnDashboardHeadNavbar : ViewComponent
    {
        UserManager _userManager = new UserManager(new EFUserRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _userManager.TGetById(writerID);
            return View(values);
        }
    }
}
