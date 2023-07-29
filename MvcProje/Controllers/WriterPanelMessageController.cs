using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
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
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelistin = mm.GetListInbox(p);
            return View(messagelistin);
        }
        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var messagelistsend = mm.GetListSendbox(p);
            return View(messagelistsend);
        }
        public ActionResult GetInBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            string sender = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(p);
            if (results.IsValid)
            {
                p.SenderMail = sender;
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