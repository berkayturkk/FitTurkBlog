using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FitTurkBlog.UI.View_Components.About
{
    [AllowAnonymous]
    public class BestBlog3PostOnAbout : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        CommentManager commentManager = new CommentManager(new EFCommentRepository());

        public IViewComponentResult Invoke()
        {
            List<BestBlogViewModel> blogViewModels = new List<BestBlogViewModel>();
            var blogList = blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).ToList();

            foreach (var blog in blogList)
            {
                var blogScoreList = commentManager.CommentGetList(blog.BlogID).Select(x => x.BlogScore).ToList();
                var totalBlogScore = blogScoreList.Sum();

                BestBlogViewModel blogViewModel = new BestBlogViewModel
                {
                    BlogID = blog.BlogID,
                    BlogTitle = blog.BlogTitle,
                    BlogContent = blog.BlogContent,
                    BlogImage = blog.BlogImage,
                    TotalBlogScore = totalBlogScore,
                    BlogCreateDate = blog.BlogCreateDate,
                    WriterName = blog.BlogWriter.NameSurname,
                    WriterImageUrl = blog.BlogWriter.ImageUrl,
                    CategoryName = blog.Category.CategoryName
                };

                blogViewModels.Add(blogViewModel);
            }

            var values = blogViewModels.OrderByDescending(x => x.TotalBlogScore).Take(3).ToList();


           
            return View(values);
        }
    }
}
