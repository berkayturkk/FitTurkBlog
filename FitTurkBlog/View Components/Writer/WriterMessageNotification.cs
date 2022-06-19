using FitTurkBlog.BL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.View_Components.Writer
{
    public class WriterMessageNotification : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
