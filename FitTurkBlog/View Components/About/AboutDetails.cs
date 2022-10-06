using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.View_Components.About
{
    public class AboutDetails : ViewComponent
    {
        AboutManager aboutManager = new AboutManager(new EFAboutRepository());

        public IViewComponentResult Invoke()
        {

            var values = aboutManager.GetList();
            return View(values);
        }
    }
}
