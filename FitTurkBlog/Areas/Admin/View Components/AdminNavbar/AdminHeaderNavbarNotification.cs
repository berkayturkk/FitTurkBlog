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
    public class AdminHeaderNavbarNotification : ViewComponent
    {
        NotificationManager _notificationManager = new NotificationManager(new EFNotificationRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public IViewComponentResult Invoke()
        {
            var values = _notificationManager.GetList().Where(x => x.NotificationStatus == true).OrderByDescending(x => x.NotificationDate).ToList();
            return View(values);
        }
    }
}
