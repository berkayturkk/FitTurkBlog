using FitTurkBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.Entities.Concrete
{
    public class Contact : IEntity
    {
        [Key]
        public int ContactID { get; set; }
        public string ContactAdress { get; set; }
        public string ContactUserName { get; set; }
        public string ContactMail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactSubject { get; set; }
        public string ContactMessage { get; set; }
        public DateTime ContactDate { get; set; }
        public bool ContactStatus { get; set; }
    }
}
