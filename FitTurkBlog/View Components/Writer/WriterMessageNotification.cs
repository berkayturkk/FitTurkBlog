using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Writer
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterMessageNotification : ViewComponent
    {
        Message2Manager _message2Manager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext _sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault(); ;
            var values = _message2Manager.GetInBoxListByWriter(writerID);
            return View(values);
        }
    }
}
