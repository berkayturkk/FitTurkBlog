using FitTurkBlog.BL.Concrete;
using FitTurkBlog.BL.ValidationRules;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using FitTurkBlog.UI.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [Authorize(Roles = "Admin,Writer")]
    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        UserManager userManager = new UserManager(new EFUserRepository());
        SqlDbContext sqlDbContext = new SqlDbContext();
        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult WriterProfile()
        {
            var userName = User.Identity.Name;
            var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            ViewBag.wi = writerID;
            var values = userManager.TGetById(writerID);
            return View(values);
        }

        public IActionResult WriterMail()
        {
            return View();
        }


        public IActionResult Test()
        {
            return View();
        }

        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult GoToAdminPanel()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile(int id)
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
        public IActionResult WriterEditProfile(AddProfileImage profileImage)
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
            user.Status = true;
            user.Email = profileImage.Email;
            userManager.Update(user);

            return RedirectToAction("WriterProfile", "Writer");
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditUserName()
        {
            UpdateUserNameViewModel userSettingModel = new UpdateUserNameViewModel();
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            userSettingModel.UpUserName = values.UserName;
            return View(userSettingModel);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditUserName(UpdateUserNameViewModel upUserSetting)
        {            
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                values.UserName = upUserSetting.UpUserName;
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "LoginUser");
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
        public IActionResult WriterEditPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditPassword(UpdatePasswordViewModel upUserSetting)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, upUserSetting.NewPassword);
                var result = await _userManager.UpdateAsync(values);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "LoginUser");
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

        [AllowAnonymous]
        public IActionResult WriterAbout(int id)
        {
            var writer = userManager.TGetById(1);
            return View(writer);
        }
    }
}
