using Microsoft.AspNetCore.Mvc;
using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        ContactManager contactManager = new ContactManager(new EFContactRepository());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contact contact)
        {
            contact.ContactDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            contact.ContactStatus = true;
            contactManager.ContactAdd(contact);
            return RedirectToAction("Index", "Contact");
        }
    }
}
