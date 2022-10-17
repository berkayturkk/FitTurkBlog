using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.Category
{
    [Authorize(Roles = "Admin,Writer")]
    public class Top3Writer : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        UserManager userManager = new UserManager(new EFUserRepository());
        public IViewComponentResult Invoke()
        {
            List<UserBlogsViewModel> userBlogs = new List<UserBlogsViewModel>();
            var writerList = userManager.GetList().Where(x => x.Status == true).ToList();
            var totalBlogCount = Convert.ToDouble(blogManager.GetList().Where(x => x.BlogStatus == true).ToList().Count);
            foreach (var writer in writerList)
            {
                var blogCount = Convert.ToDouble(blogManager.GetBlogListByWriter(writer.Id).Where(x => x.BlogStatus == true).ToList().Count);

                UserBlogsViewModel model = new UserBlogsViewModel
                {
                    BlogCount = blogCount,
                    ID = writer.Id,
                    NameSurname = writer.NameSurname,
                    Percent = (Math.Round(((blogCount / totalBlogCount) * 100))).ToString()
                    
                };

                userBlogs.Add(model);
            }

            var values = userBlogs.OrderByDescending(x => x.BlogCount).Take(3).ToList();

            return View(values);
        }
    }
}
