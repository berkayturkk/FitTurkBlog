using FitTurkBlog.BL.Concrete;
using FitTurkBlog.DAL.Context;
using FitTurkBlog.DAL.EntityFramework;
using FitTurkBlog.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {
        Message2Manager _message2Manager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext _sqlDbContext = new SqlDbContext();
        public IActionResult Inbox()
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Manager.GetInBoxListByWriter(writerID).OrderByDescending(x => x.MessageID).ToList(); 
            return View(values);
        }
        public IActionResult Sendbox()
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values = _message2Manager.GetSendBoxListByWriter(writerID).OrderByDescending(x => x.MessageID).ToList();
            return View(values);
        }

        public IActionResult MessageDetails(int id)
        {
            var value = _message2Manager.TGetById(id);
            return View(value);
        }
        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _sqlDbContext.Users.ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            List<SelectListItem> recieverUsers = (from x in await GetUsersAsync()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Email.ToString(),
                                                      Value = x.Id.ToString()
                                                  }).ToList();
            ViewBag.mv = recieverUsers;
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 message2)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            
            message2.MessageSenderID = writerID;
            message2.MessageReceiverID = 1;
            message2.MessageStatus = true;
            message2.MessageDate = Convert.ToDateTime(DateTime.Now);
            if (message2.MessageSubject != null && message2.MessageDetails != null)
            {
                _message2Manager.Add(message2);
                return RedirectToAction("SendBox", "Message");
            }
            return RedirectToAction("SendMessage", "Message");
        }

        public IActionResult DeleteMessage(int id)
        {
            var messageValue = _message2Manager.TGetById(id);
            _message2Manager.Delete(messageValue);
            return RedirectToAction("InBox", "Message");
        }
    }
}
