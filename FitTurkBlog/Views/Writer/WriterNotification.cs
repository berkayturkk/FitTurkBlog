using Microsoft.AspNetCore.Mvc;

namespace FitTurkBlog.UI.Views.Writer
{
    public class WriterNotification : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
