using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        [Authorize]
        public ActionResult Inbox()
        {//Gelen Mesajları Listeledim

            string adminUserName = (string)Session["AdminUserName"];
            var messageList = mm.GetListInbox(adminUserName);
            ViewBag.okunmamış = mm.GetUnreadMessageCount(adminUserName);

            return View(messageList);
        }
        public ActionResult Sendbox(string p)
        {
            var messagelistsend = mm.GetListSendbox(p);
            return View(messagelistsend);
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            if (values.IsRead==false)
            {
                values.IsRead = true;
                mm.MessageUpdate(values);
            }
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messageValidator.Validate(p);
            if (results.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                mm.MessageAdd(p);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        //public ActionResult ChangedIsRead(int id)
        //{
        //    var message = mm.GetById(id);

        //    if (message.IsRead)
        //    {
        //        message.IsRead = false;
        //    }
        //    else
        //    {
        //        message.IsRead = true;
        //    }
        //    mm.MessageUpdate(message);
        //    return RedirectToAction("Inbox");
        //}

        public ActionResult ChangedIsRead(int id)
        {
            var message = mm.GetById(id);
            message.IsRead = true;
            mm.MessageUpdate(message);
            return RedirectToAction("Inbox");
        }

        public ActionResult ChangedUnRead(int id)
        {
            var message = mm.GetById(id);
            message.IsRead = false;
            mm.MessageUpdate(message);
            return RedirectToAction("Inbox");
        }
    }
}