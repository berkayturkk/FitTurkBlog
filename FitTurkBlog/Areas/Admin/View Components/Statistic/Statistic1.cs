using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace FitTurkBlog.UI.Areas.Admin.View_Components.Statistic
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class Statistic1:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        Message2Manager message2Manager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();

            ViewBag.vBlogCount = blogManager.GetList().Count();
            ViewBag.vMessageCount = message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList().Count();
            ViewBag.vCommentCount = sqlDbContext.Comments.Count();

            string api = "14ad2aba611dbef9c504b82a127794c5";
            string connection = "http://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid=" + api;
            XDocument document = XDocument.Load(connection);
            ViewBag.vWeather = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
