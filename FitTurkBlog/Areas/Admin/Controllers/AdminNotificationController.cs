using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminNotificationController : Controller
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
                var values = notificationManager.GetListNotificationByKey(key).OrderByDescending(x => x.NotificationDate).ToPagedList(page, 10);
                return View(values);
            }
            else
            {
                var values = notificationManager.GetList().OrderByDescending(x => x.NotificationDate).ToPagedList(page, 10);
                return View(values);
            }
        }

        public IActionResult ChangeStatusNotification(int id)
        {
            var value = notificationManager.TGetById(id);
            if (value.NotificationStatus)
            {
                value.NotificationStatus = false;
            }
            else
            {
                value.NotificationStatus = true;
            }
            notificationManager.Update(value);

            return RedirectToAction("AllNotification");
        }

        [HttpGet]
        public IActionResult AddNotification()
        {
            List<SelectListItem> typeSembol = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "mdi mdi-calendar",
                    Value = "mdi mdi-calendar"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-link-variant",
                    Value = "mdi mdi-link-variant"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-settings",
                    Value = "mdi mdi-settings"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-bookmark-plus-outline",
                    Value = "mdi mdi-bookmark-plus-outline"
                }

            }.ToList();

            List<SelectListItem> typeSembolColor = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "preview-icon bg-success",
                    Value = "preview-icon bg-success"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-warning",
                    Value = "preview-icon bg-warning"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-info",
                    Value = "preview-icon bg-info"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-primary",
                    Value = "preview-icon bg-primary"
                }

            }.ToList();

            ViewBag.ts = typeSembol;
            ViewBag.tsc = typeSembolColor;

            return View();
        }

        [HttpPost]
        public IActionResult AddNotification(Notification notification)
        {
            notification.NotificationStatus = true;
            notification.NotificationDate = Convert.ToDateTime(DateTime.Now);
            if (notification.NotificationType != null && notification.NotificationDetails != null && notification.NotificationTypeSymbol != null && notification.NotificationTypeSymbolColor != null)
            {
                notificationManager.Add(notification);
                return RedirectToAction("AllNotification", "AdminNotification");
            }
            return RedirectToAction("AddNotification", "AdminNotification");
        }

        public IActionResult DeleteNotification(int id)
        {
            var messageValue = notificationManager.TGetById(id);
            notificationManager.Delete(messageValue);
            return RedirectToAction("AllNotification", "AdminNotification");
        }

        [HttpGet]
        public IActionResult EditNotification(int id)
        {
            var value = notificationManager.TGetById(id);
            TempData["notificationID"] = value.NotificationID;

            List<SelectListItem> typeSembol = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "mdi mdi-calendar",
                    Value = "mdi mdi-calendar"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-link-variant",
                    Value = "mdi mdi-link-variant"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-settings",
                    Value = "mdi mdi-settings"
                },
                new SelectListItem
                {
                    Text = "mdi mdi-bookmark-plus-outline",
                    Value = "mdi mdi-bookmark-plus-outline"
                }

            }.ToList();

            List<SelectListItem> typeSembolColor = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "preview-icon bg-success",
                    Value = "preview-icon bg-success"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-warning",
                    Value = "preview-icon bg-warning"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-info",
                    Value = "preview-icon bg-info"
                },
                new SelectListItem
                {
                    Text = "preview-icon bg-primary",
                    Value = "preview-icon bg-primary"
                }

            }.ToList();

            ViewBag.ts = typeSembol;
            ViewBag.tsc = typeSembolColor;

            return View(value);
        }

        [HttpPost]
        public IActionResult EditNotification(Notification notification)
        {
            notification.NotificationStatus = true;
            notification.NotificationID = (int)TempData["notificationID"];
            notification.NotificationDate = Convert.ToDateTime(DateTime.Now);
            notificationManager.Update(notification);
            return RedirectToAction("AllNotification", "AdminNotification");
        }
    }
}
