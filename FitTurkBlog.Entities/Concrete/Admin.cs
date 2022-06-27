using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.Entities.Concrete
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }
        public string AdminName { get; set; }
        public string AdminShortDescription { get; set; }
        public string AdminImage { get; set; }
        public string AdminRole { get; set; }
        public bool AdminStatus { get; set; }

    }
}
