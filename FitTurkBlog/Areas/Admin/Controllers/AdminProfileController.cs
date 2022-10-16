using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        UserManager userManager = new UserManager(new EFUserRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public AdminProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            ViewBag.wi = writerID;
            var values = userManager.TGetById(writerID);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
        {
            AddProfileImage profileImage = new AddProfileImage();
            var value = await _userManager.FindByIdAsync((id).ToString());
            profileImage.NameSurname = value.NameSurname;
            profileImage.Username = value.UserName;
            profileImage.About = value.About;
            profileImage.Status = value.Status;
            profileImage.Email = value.Email;
            return View(profileImage);
        }

        [HttpPost]
        public ActionResult EditProfile(AddProfileImage profileImage)
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
                user.ImageUrl = "/FitTurkBlog/newImages/" + newImageName;
                ViewBag.iu = "/FitTurkBlog/newImages/" + newImageName;
            }

            user.NameSurname = profileImage.NameSurname;
            user.UserName = profileImage.Username;
            user.About = profileImage.About;
            user.Status = profileImage.Status;
            user.Email = profileImage.Email;
            userManager.Update(user);

            return RedirectToAction("Index", "AdminProfile");
        }
    }
}
