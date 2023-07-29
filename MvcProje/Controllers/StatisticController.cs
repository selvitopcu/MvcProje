using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProje.Controllers
{
    [AllowAnonymous]
    public class StatisticController : Controller
    {
        Context c = new Context();  
        // GET: Statistic
        public ActionResult Index()
        {
            var toplamkategori = c.Categories.Count().ToString();
            var yazilimbasliksayisi = c.Headings.Count(y => y.Category.CategoryName == "Yazılım").ToString();
            var yazaradi = c.Writers.Count(x => x.WriterName.Contains("a")).ToString();

            var encokbaslikolankatadi = c.Categories.Where(x => x.CategoryID == (c.Headings.GroupBy(y => y.CategoryID).OrderByDescending(z => z.Count()).Select(z => z.Key).FirstOrDefault())).Select(k => k.CategoryName).FirstOrDefault();

            var kategoridurumfarklari = c.Categories.Count(x => x.CategoryStatus == true) - c.Categories.Count(x => x.CategoryStatus == false);



            ViewBag.toplamkat = toplamkategori;
            ViewBag.yazilimbaslik = yazilimbasliksayisi;
            ViewBag.yazarad = yazaradi;
            ViewBag.kategorisay = encokbaslikolankatadi;
            ViewBag.katdurumfark = kategoridurumfarklari;
            return View();
        }
    }
}