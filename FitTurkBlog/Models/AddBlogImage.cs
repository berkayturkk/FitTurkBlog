using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace FitTurkBlog.UI.Models
{
    public class AddBlogImage
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public IFormFile BlogThumbnailImage { get; set; }
        public IFormFile BlogImage { get; set; }
        public int CategoryID { get; set; }
        public bool BlogStatus { get; set; }
        public int WriterID { get; set; }
    }
}
