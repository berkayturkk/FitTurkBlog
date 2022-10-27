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
    public class MostCommentedBlog3PostOnAbout : ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EFBlogRepository());
        CommentManager commentManager = new CommentManager(new EFCommentRepository());

        public IViewComponentResult Invoke()
        {
            List<BestBlogViewModel> blogViewModels = new List<BestBlogViewModel>();
            var blogList = blogManager.GetBlogListWithCategoryWriter().Where(x => x.BlogStatus == true).ToList();

            foreach (var blog in blogList)
            {
                var blogCommentList = commentManager.CommentGetList(blog.BlogID).ToList();
                var totalBlogComment = blogCommentList.Count();

                BestBlogViewModel blogViewModel = new BestBlogViewModel
                {
                    BlogID = blog.BlogID,
                    BlogTitle = blog.BlogTitle,
                    BlogContent = blog.BlogContent,
                    BlogImage = blog.BlogImage,
                    BlogCreateDate = blog.BlogCreateDate,
                    WriterID = blog.BlogWriterId,
                    WriterName = blog.BlogWriter.NameSurname,
                    WriterImageUrl = blog.BlogWriter.ImageUrl,
                    CategoryName = blog.Category.CategoryName,
                    CommentCount = totalBlogComment
                };

                blogViewModels.Add(blogViewModel);
            }

            var values = blogViewModels.OrderByDescending(x => x.CommentCount).Take(3).ToList();



            return View(values);
        }
    }
}
