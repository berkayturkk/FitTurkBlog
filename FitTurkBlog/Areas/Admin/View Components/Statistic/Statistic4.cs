using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Statistic4 : ViewComponent
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EFCategoryRepository());
        CommentManager commentManager = new CommentManager(new EFCommentRepository());
        UserManager userManager = new UserManager(new EFUserRepository());
        Message2Manager messageManager = new Message2Manager(new EFMessage2Repository());
        public IViewComponentResult Invoke()
        {

            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();

            ViewBag.vAdminName = sqlDbContext.Users.Where(x => x.Id == writerID).Select(y => y.NameSurname).FirstOrDefault();
            ViewBag.vAdminImage = sqlDbContext.Users.Where(x => x.Id == writerID).Select(y => y.ImageUrl).FirstOrDefault();
            ViewBag.vAdminShortDescription = sqlDbContext.Users.Where(x => x.Id == writerID).Select(y => y.About).FirstOrDefault();
            ViewBag.vNotification = notificationManager.GetList().Where(x => x.NotificationStatus == true).Count();

            ViewBag.vBlogs = blogManager.GetList().Where(x => x.BlogStatus == true).ToList().Count();
            ViewBag.vCategories = categoryManager.GetList().Where(x => x.CategoryStatus == true).Count();
            ViewBag.vComments = commentManager.GetListAllComment().Where(x => x.CommentStatus == true).Count();
            ViewBag.vWriters = userManager.GetList().Where(x => x.Status == true).ToList().Count();
            ViewBag.vMessages = messageManager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList().Count();

            return View();
        }
    }
}
