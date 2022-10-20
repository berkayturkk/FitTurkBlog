using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace FitTurkBlog.UI.Models
{
    public class AddProfileImage
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string About { get; set; }
        public bool Status { get; set; }

    }
}
