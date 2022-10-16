using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.AdminNotification
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminNotification : ViewComponent
    {
        NotificationManager _notificationManager = new NotificationManager(new EFNotificationRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public IViewComponentResult Invoke()
        {
            var values = _notificationManager.GetList().Where(x => x.NotificationStatus == true).ToList();
            ViewBag.nc = values.Count();
            return View();
        }
    }
}
