using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Notification
{
    [Authorize(Roles = "Writer,Admin")]
    public class WriterNavbarNotification : ViewComponent
    {
        NotificationManager notificationManager = new NotificationManager(new EFNotificationRepository());
        public IViewComponentResult Invoke()
        {

            var values = notificationManager.GetList().Where(x => x.NotificationStatus == true && x.NotificationType != "Yeni Kayıt").ToList();
            return View(values);
        }
    }
}
