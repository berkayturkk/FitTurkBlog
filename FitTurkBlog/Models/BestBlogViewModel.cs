using System;

namespace FitTurkBlog.UI.Models
{
    public class BestBlogViewModel
    {
        public int BlogID { get; set; }    
        public string BlogTitle { get; set; } 
        public string BlogContent { get; set; }
        public string BlogImage { get; set; }  
        public DateTime BlogCreateDate { get; set; }
        public int TotalBlogScore { get; set; }
        public string WriterName { get; set; }
        public string WriterImageUrl { get; set; }
        public string CategoryName { get; set; }
        public int CommentCount { get; set; }

    }
}
