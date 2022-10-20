using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using X.PagedList;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;


namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminWriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        UserManager userManager = new UserManager(new EFUserRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public AdminWriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1)
        {
            var values = userManager.GetList().OrderByDescending(x => x.Id).ToPagedList(page, 2);
            return View(values);
        }

        public IActionResult ChangeStatusWriter(int id)
        {
            var value = userManager.TGetById(id);
            if (value.Status)
            {
                value.Status = false;
            }
            else
            {
                value.Status = true;
            }
            userManager.Update(value);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWriter(AppUser user)
        {
            if(ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                var newPassword = guid.ToString().Substring(0, 8);

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("fitturkblog@gmail.com", "mehblijlpxajxrrl");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;

                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress("fitturkblog@gmail.com", "FITTURKBLOG");
                mail.IsBodyHtml = true;
                mail.Subject = "Kayıt Olma";
                mail.Body += "<h3>Merhaba " + user.NameSurname + ",</h3>" + "<p>FitTurkBlog Hesabınız ekibimiz tarafından açılmıştır.</p>" + "<h4>Kullanıcı Adı = " + user.UserName + "</h4><h4>Şifre = " + newPassword + "</h4><p>Kayıt olma talebinizi gerçekleştirdik. Yukarıda belirtilen kullanıcı adı ve şifre ile giriş yapabilirsiniz.</p><p>FitTurkBlog sitesinde sizleri de görmekten mutluluk duyduk. Hoşgeldiniz.</p><p>FitTürkBlog Ekibi</p>";

                client.Send(mail);
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                user.Status = true;
                user.ImageUrl = "/FitTurkBlog/newImages/uyeresimyok.jpg";
                var result = await _userManager.CreateAsync(user, newPassword);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "AdminWriter");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);

        }
        public IActionResult DeleteWriter(int id)
        {
            var value = userManager.TGetById(id);
            userManager.Delete(value);
            return RedirectToAction("Index", "AdminWriter");
        }

        public IActionResult WriterDetail(int id, int page = 1)
        {
            var value = userManager.TGetById(id);
            ViewBag.wi = id;
            return View(value);
        }

        [HttpGet]
        public async Task<IActionResult> EditWriter(int id)
        {
            AddProfileImage profileImage = new AddProfileImage();
            var value = await _userManager.FindByIdAsync((id).ToString());
            profileImage.NameSurname = value.NameSurname;
            profileImage.About = value.About;
            profileImage.Status = value.Status;
            profileImage.Email = value.Email;
            return View(profileImage);
        }

        [HttpPost]
        public ActionResult EditWriter(AddProfileImage profileImage)
        {
            var mail = profileImage.Email;
            var user = sqlDbContext.Users.Where(x => x.Email == mail).FirstOrDefault();
            if (profileImage.ImageUrl != null)
            {
                var extension = Path.GetExtension(profileImage.ImageUrl.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/FitTurkBlog/newImages/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                profileImage.ImageUrl.CopyTo(stream);
                user.ImageUrl = "/FitTurkBlog/newImages/"+newImageName;
                ViewBag.iu = "/FitTurkBlog/newImages/" + newImageName; 
            }

            user.NameSurname = profileImage.NameSurname;
            user.About = profileImage.About;
            user.Status = profileImage.Status;
            user.Email = profileImage.Email;
            userManager.Update(user);

            return RedirectToAction("Index", "AdminWriter");
        }

    
    }
}
