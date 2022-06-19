using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FitTurkBlog.UI.View_Components
{
    public class CommentList:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var commentValues = new List<UserComment>
            {
                new UserComment
                {
                    ID = 1,
                    UserName = "Mehmet Ali OKUDAN"
                },
                new UserComment
                {
                    ID= 2,
                    UserName = "Furkan TAŞÇI"
                },
                new UserComment
                {
                    ID= 3,
                    UserName = "Osman BAŞ"
                }
            };

            return View(commentValues);
        }
    }
}
