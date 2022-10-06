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
    public class WriterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        WriterManager writerManager = new WriterManager(new EFWriterRepository());
        UserManager userManager = new UserManager(new EFUserRepository());

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userMail = User.Identity.Name;
            ViewBag.um = userMail;
            SqlDbContext sqlDbContext = new SqlDbContext();
            var writerName = sqlDbContext.Writers.Where(x => x.WriterMail == userMail).Select(y => y.WriterName).FirstOrDefault();
            ViewBag.wn = writerName;
            return View();
        }

        public IActionResult WriterProfile()
        {
            return View();
        }

        public IActionResult WriterMail()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }

        [AllowAnonymous]
        public PartialViewResult WriterNavbarPartial()
        {
            return PartialView();
        }

        [AllowAnonymous]
        public PartialViewResult WriterFooterPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> WriterEditProfile()
        {
            //var userName = User.Identity.Name;
            //SqlDbContext sqlDbContext = new SqlDbContext();
            //var userMail = sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            //var ID = sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            //var Values = userManager.TGetById(ID);
            //return View(Values);
            UserUpdateViewModel userModel = new UserUpdateViewModel();
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            userModel.UpNameSurname = values.NameSurname;
            userModel.UpMail = values.Email;
            userModel.UpImageUrl = values.ImageUrl;
            userModel.UpAbout = values.About;

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> WriterEditProfile(UserUpdateViewModel upUser)
        {
            //WriterValidator writerValidator = new WriterValidator();
            //ValidationResult validationResult = writerValidator.Validate(writer);
            //if(validationResult.IsValid)
            //{
            //    writerManager.Update(writer);
            //    return RedirectToAction("WriterEditProfile", "Writer");
            //}
            //else
            //{
            //    foreach (var item in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}
            //return View();

            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            values.NameSurname = upUser.UpNameSurname;
            values.Email = upUser.UpMail;
            values.ImageUrl = upUser.UpImageUrl;
            values.About = upUser.UpAbout;
            var result = await _userManager.UpdateAsync(values);
            return RedirectToAction("Index", "Dashboard");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult WriterAdd()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult WriterAdd (AddProfileImage addProfileImage)
        {
            Writer writer = new Writer();
            if(addProfileImage.WriterImage != null)
            {
                var extension = Path.GetExtension(addProfileImage.WriterImage.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WriterImageFiles/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                addProfileImage.WriterImage.CopyTo(stream);
                writer.WriterImage = newimagename;
            }
            writer.WriterMail = addProfileImage.WriterMail;
            writer.WriterName = addProfileImage.WriterName;
            writer.WriterPassword = addProfileImage.WriterPassword;
            writer.WriterStatus = true;
            writer.WriterAbout = addProfileImage.WriterAbout;
            writerManager.Add(writer);
            return RedirectToAction("WriterEditProfile", "Writer");
        }


    }
}
