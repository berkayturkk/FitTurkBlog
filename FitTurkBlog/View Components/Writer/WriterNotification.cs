using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Writer
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterNotification : ViewComponent
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());

        public IViewComponentResult Invoke()
        {

            var values = notificationManager.GetList().Where(x => x.NotificationStatus == true && x.NotificationType != "Yeni Kayıt").OrderByDescending(x => x.NotificationDate).Take(5).ToList();
            return View(values);
        }
    }
}
