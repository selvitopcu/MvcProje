﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv= new ContactValidator();
        // GET: Context
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }
        public ActionResult GetContactDetails(int id)
        {
            var contactvalues=cm.GetById(id);
            return View(contactvalues);
        }
        public PartialViewResult MessageListMenu()
        {
            string adminUserName = (string)Session["AdminUserName"];
            ViewBag.okunmamış = mm.GetUnreadMessageCount(adminUserName);
            return PartialView();

        }
    }
}