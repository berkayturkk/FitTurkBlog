using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Areas.Admin.Models;
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
        private readonly SignInManager<AppUser> _signInManager;
        UserManager userManager = new UserManager(new EFUserRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();

        public AdminProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            ViewBag.ai = writerID;
            var values = userManager.TGetById(writerID);
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int id)
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
        public IActionResult EditProfile(AddProfileImage profileImage)
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
            user.About = profileImage.About;
            user.Status = profileImage.Status;
            user.Email = profileImage.Email;
            userManager.Update(user);

            return RedirectToAction("Index", "AdminProfile");
        }

        [HttpGet]
        public async Task<IActionResult> AdminEditUserName()
        {
            AdminUpdateUserNameViewModel userSettingModel = new AdminUpdateUserNameViewModel();
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            userSettingModel.UpUserName = values.UserName;
            return View(userSettingModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdminEditUserName(AdminUpdateUserNameViewModel upUserSetting)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                values.UserName = upUserSetting.UpUserName;
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "LoginUser", new { area = "" });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult AdminEditPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminEditPassword(AdminUpdatePasswordViewModel upUserSetting)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, upUserSetting.NewPassword);
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "LoginUser", new { area = "" });
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "LoginUser",new {area = ""});
        }

    }
}
