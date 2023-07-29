using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    public class SkillController : Controller
    {
        SkillManager skillManager = new SkillManager(new EfSkillDal());
        // GET: Skill
        public ActionResult Index()
        {
            var values = skillManager.GetList();
            return View(values);
        }
        public ActionResult Index2()
        {
            var values = skillManager.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSkill(Skill p)
        {
            skillManager.SkillAdd(p);
            return RedirectToAction("Index2");
        }
     
        public ActionResult DeleteSkill(int id)
        { 
            var skill= skillManager.GetById(id);
            skillManager.SkillDelete(skill);
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public ActionResult UpdateSkill(int id)
        {
            var skill = skillManager.GetById(id);
            return View(skill);
        }
        [HttpPost]
        public ActionResult UpdateSkill(Skill p)
        {
            skillManager.SkillUpdate(p);
            return RedirectToAction("Index2");
        }

    }
}