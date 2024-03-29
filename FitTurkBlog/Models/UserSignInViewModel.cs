﻿using System.ComponentModel.DataAnnotations;

namespace FitTurkBlog.UI.Models
{
    public class UserSignInViewModel
    {

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz !")]
        public string UserName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz !")]
        public string Password { get; set; }

        public virtual bool IsRememberMe { get; set; }
    }
}
