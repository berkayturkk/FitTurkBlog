using FitTurkBlog.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public string NameSurname { get; set; }
        public string ImageUrl { get; set; }
        public string About { get; set; }
        public bool Status { get; set; }
        public List<Blog> WriterBlogs { get; set; }
        public virtual ICollection<Message2> WriterSender { get; set; }
        public virtual ICollection<Message2> WriterReceiver { get; set; }
    }
}
