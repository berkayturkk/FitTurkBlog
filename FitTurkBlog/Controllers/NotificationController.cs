using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

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

        public IActionResult AllNotification(string key,int page = 1)
        {
            if(key != null)
            {
                var values = notificationManager.GetListNotificationByKey(key).Where(x => x.NotificationStatus == true).OrderByDescending(x => x.NotificationDate).ToPagedList(page, 10);
                return View(values);
            }
            else
            {
                var values = notificationManager.GetList().Where(x => x.NotificationStatus == true).OrderByDescending(x => x.NotificationDate).ToPagedList(page, 10);
                return View(values);
            }
        }

        public IActionResult NotificationDetails(int id)
        {
            var values = notificationManager.TGetById(id);
            return View(values);
        }
    }
}
