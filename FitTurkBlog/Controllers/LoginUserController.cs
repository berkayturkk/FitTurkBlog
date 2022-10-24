using FitTurkBlog.DAL.Context;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class LoginUserController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        SqlDbContext _sqlDbContext = new SqlDbContext();

        public LoginUserController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.UserName, p.Password, p.IsRememberMe, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "LoginUser");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "LoginUser");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(UserResetPassword userResetPassword)
        {
            var _email = userResetPassword.URPEmail;
            var values = _sqlDbContext.Users.FirstOrDefault(x => x.Email == _email);

            if (values != null)
            {
                Guid guid = Guid.NewGuid();
                var newPassword = guid.ToString().Substring(0, 8);

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("fitturkblog@gmail.com", "mehblijlpxajxrrl");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.To.Add(_email);
                mail.From = new MailAddress("fitturkblog@gmail.com", "FITTURKBLOG");
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Sıfırlama";
                mail.Body += "<h3>Merhaba " + values.NameSurname + ",</h3>" + "<p>FitTurkBlog Hesabınız için bir şifre sıfırlama talebi aldık.</p>" + "<h4>Kullanıcı Adı = "+values.UserName+"</h4><h4>Şifre = "+newPassword+ "</h4><p>Sıfırlama talebinizi gerçekleştirdik. Yukarıda belirtilen kullanıcı adı ve şifre ile giriş yapabilirsiniz.</p><p>Hesabınızı korumak için bize verdiğiniz destekten dolayı teşekkür ederiz.</p><p>FitTürkBlog Ekibi</p>";

                try
                {
                    client.Send(mail);
                    values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, newPassword);
                    _sqlDbContext.SaveChanges();
                    return RedirectToAction("Index", "LoginUser");
                }
                catch (Exception)
                {

                    throw;
                }


            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return PartialView();
        }

    }
}

