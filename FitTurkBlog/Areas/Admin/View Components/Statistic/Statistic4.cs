using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    public class Statistic4 : ViewComponent
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.vAdminName = sqlDbContext.Admins.Where(x => x.AdminID == 1).Select(y => y.AdminName).FirstOrDefault();
            ViewBag.vAdminImage = sqlDbContext.Admins.Where(x => x.AdminID == 1).Select(y => y.AdminImage).FirstOrDefault();
            ViewBag.vAdminShortDescription = sqlDbContext.Admins.Where(x => x.AdminID == 1).Select(y => y.AdminShortDescription).FirstOrDefault();
            return View();
        }
    }
}
