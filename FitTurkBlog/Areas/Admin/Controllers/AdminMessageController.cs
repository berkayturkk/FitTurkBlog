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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FitTurkBlog.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMessageController : Controller
    {
        Message2Manager _message2Manager = new Message2Manager(new EFMessage2Repository());
        SqlDbContext _sqlDbContext = new SqlDbContext();
        public IActionResult Inbox(string key)
        {

            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();

            if (key != null)
            {
                var values = _message2Manager.GetInBoxListByKey(writerID, key).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
            else
            {
                var values = _message2Manager.GetInBoxListByWriter(writerID).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
        }

        public IActionResult SendBox(string key)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();
            if (key != null)
            {
                var values = _message2Manager.GetSendBoxListByKey(writerID, key).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
            else
            {
                var values = _message2Manager.GetSendBoxListByWriter(writerID).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
        }

        public async Task<List<AppUser>> GetUsersAsync()
        {
            return await _sqlDbContext.Users.ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values3 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values3.Count();
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
            //message2.MessageReceiverID = 5;
            message2.MessageStatus = true;
            message2.MessageDate = Convert.ToDateTime(DateTime.Now);
            if (message2.MessageSubject != null && message2.MessageDetails != null)
            {
                _message2Manager.Add(message2);
                return RedirectToAction("SendBox", "AdminMessage");
            }
            return RedirectToAction("SendMessage", "AdminMessage");
        }

        public IActionResult AllMessage(string key)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            ViewBag.vWriterID = writerID;
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();
            if (key != null)
            {
                var values = _message2Manager.GetListAllByKey(key).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
            else
            {
                var values = _message2Manager.GetListAllMessage();
                return View(values);
            }

        }

        public IActionResult InboxMessageDetail(int id)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var value = _message2Manager.TGetById(id);
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();

            return View(value);
        }

        public IActionResult MessageDetail(int id)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var value = _message2Manager.TGetById(id);
            value.MessageStatus = false;
            _message2Manager.Update(value);
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();

            return View(value);
        }

        public IActionResult MoveToTrash(int id)
        {
            var messageValue = _message2Manager.TGetById(id);
            if (messageValue.IsDeleted == false)
            {
                if (messageValue.IsImportant == true)
                {
                    messageValue.IsDeleted = true;
                    messageValue.IsImportant = false;
                    messageValue.MessageStatus = false;
                    _message2Manager.Update(messageValue);
                }
                else
                {
                    messageValue.IsDeleted = true;
                    messageValue.MessageStatus = false;
                    _message2Manager.Update(messageValue);
                }

            }
            else
            {
                messageValue.IsDeleted = false;
                messageValue.MessageStatus = false;
                _message2Manager.Update(messageValue);
            }
            return RedirectToAction("GetListTrash", "AdminMessage");
        }

        public IActionResult GetListTrash(string key)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();
            if (key != null)
            {
                var values = _message2Manager.GetTrashBoxListByKey(key).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
            else
            {
                var values = _message2Manager.GetTrashBoxListByWriter().OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
        }

        public IActionResult IsImportant(int id)
        {
            var value = _message2Manager.TGetById(id);
            if (value.IsImportant == false)
            {
                if (value.IsDeleted == true)
                {
                    value.IsImportant = true;
                    value.MessageStatus = false;
                    value.IsDeleted = false;
                    _message2Manager.Update(value);
                }
                else
                {
                    value.IsImportant = true;
                    value.MessageStatus = false;
                    _message2Manager.Update(value);
                }
            }
            else
            {
                value.IsImportant = false;
                _message2Manager.Update(value);
            }
            return RedirectToAction("GetListImportant", "AdminMessage");
        }

        public IActionResult GetListImportant(string key)
        {
            var userName = User.Identity.Name;
            var userMail = _sqlDbContext.Users.Where(x => x.UserName == userName).Select(y => y.Email).FirstOrDefault();
            var writerID = _sqlDbContext.Users.Where(x => x.Email == userMail).Select(y => y.Id).FirstOrDefault();
            var values2 = _message2Manager.GetInBoxListByWriter(writerID).Where(x => x.MessageStatus == true).ToList();
            ViewBag.gm = values2.Count();
            if (key != null)
            {
                var values = _message2Manager.GetImportantBoxListByKey(key).OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
            else
            {
                var values = _message2Manager.GetImportantBoxListByWriter().OrderByDescending(x => x.MessageDate).ToList();
                return View(values);
            }
        }

        public IActionResult IsRead(int id)
        {
            var value = _message2Manager.TGetById(id);
            if (value.MessageStatus == false)
            {
                value.MessageStatus = true;
                _message2Manager.Update(value);
            }
            else
            {
                value.MessageStatus = false;
                _message2Manager.Update(value);
            }
            return RedirectToAction("Inbox", "AdminMessage");
        }

        public IActionResult DeleteMessage(int id)
        {
            var messageValue = _message2Manager.TGetById(id);
            _message2Manager.Delete(messageValue);
            return RedirectToAction("Inbox", "AdminMessage");
        }


    }
}
