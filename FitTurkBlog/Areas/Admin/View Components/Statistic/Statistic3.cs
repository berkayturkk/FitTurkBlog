using FitTurkBlog.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Statistic3 : ViewComponent
    {
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
