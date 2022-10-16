using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminNavbar
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSideNavbarMessage : ViewComponent
    {
        Message2Manager _messageManager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _messageManager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            return View(values);
        }
    }
}
