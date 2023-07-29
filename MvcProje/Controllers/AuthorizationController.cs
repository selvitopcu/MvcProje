using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concreate;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        AdminManager am=new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var adminvalues = am.GetList();
            return View(adminvalues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(Admin p)
        {
            am.AdminAdd(p);
            return RedirectToAction("Index");
        }


        //public ActionResult DeleteAdmin(int id)
        //{
        //    var categoryvalue = cm.GetById(id);
        //    cm.CategoryDelete(categoryvalue);
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminvalue = am.GetById(id);
            return View(adminvalue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            am.AdminUpdate(p);
            return RedirectToAction("Index");
        }
    }
}