using kundt_front_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kundt_front_end.Controllers
{
    public class HomeController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Step2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Step2(string date_von, string date_bis)
        {
            //ViewBag.date_von = date_von;
            //ViewBag.date_bis = date_bis;
            System.Web.HttpContext.Current.Session["sessionDate_von"] = date_von;
            System.Web.HttpContext.Current.Session["sessionDate_bis"] = date_bis;
            //ViewBag.dateVon = System.Web.HttpContext.Current.Session["sessionDate_von"];
            //ViewBag.dateBis = System.Web.HttpContext.Current.Session["sessionDate_bis"];

            if (!string.IsNullOrEmpty(date_von) && !string.IsNullOrEmpty(date_bis))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Step3(int? id)
        {
            //ViewBag.dateVon = System.Web.HttpContext.Current.Session["sessionDate_von"];
            //ViewBag.dateBis = System.Web.HttpContext.Current.Session["sessionDate_bis"];

            tblAutoFrontEndController afec = new tblAutoFrontEndController();
            return View(afec.Step3b(id));
        }

        //[HttpPost, ActionName("Rücktrittsversicherung")]
        //public void Insurance(int? id) //Versicherung CheckBox
        //{
        //    tblBuchung bu = db.tblBuchung.Find(id);
        //    if (bu.Versicherung == true)
        //    {
        //        bu.Versicherung = false;
        //    }
        //    else
        //    {
        //        bu.Versicherung = true;
        //    }

        //    if (ViewBag.VersStatus == true)
        //    {
        //        ViewBag.VersStatus = "Oh Noez...";
        //    }
        //    else
        //    {
        //        ViewBag.VersStatus = "blöd gelaufen...";
        //    }
        //    db.SaveChanges();
        //    ViewBag.VersStatus = bu.Versicherung;
        //}
        public ActionResult Step4(int? id)
        {
            //Wenn eingeloggt dann diesen Step überspringen
            if (System.Web.HttpContext.Current.Session["IDUser"]  != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                return RedirectToAction("Step5");
            }
            tblAutoFrontEndController afec2 = new tblAutoFrontEndController();
            return View(afec2.Step3b(id));
        }
        public ActionResult Step5()
        {
            return View();
        }
        public ActionResult Step6()
        {
            return View();
        }
        public ActionResult Impressum()
        {
            return View();
        }
        public ActionResult AGB()
        {
            return View();
        }
    }
}