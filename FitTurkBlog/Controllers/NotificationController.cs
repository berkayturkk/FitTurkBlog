using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class NotificationController : Controller
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllNotification()
        {
            var values = notificationManager.GetList().Where(x => x.NotificationStatus == true).OrderByDescending(x => x.NotificationDate).ToList();
            return View(values);
        }
    }
}
