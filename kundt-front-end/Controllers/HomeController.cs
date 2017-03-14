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
        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// GET: Home/Step2
        /// </summary>
        /// <returns></returns>
        public ActionResult Step2()
        {
            return View();
        }
        /// <summary>
        /// POST: Home/Step2
        /// </summary>
        /// <param name="date_von"></param>
        /// <param name="date_bis"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step2(DateTime ? date_von, DateTime ? date_bis)
        {

            //ViewBag.date_von = date_von;
            //ViewBag.date_bis = date_bis;
            if (date_von != null && date_bis != null)
            {
                System.Web.HttpContext.Current.Session["sessionDate_von"] = date_von;
                System.Web.HttpContext.Current.Session["sessionDate_bis"] = date_bis;
                ViewBag.Dauer = System.Web.HttpContext.Current.Session["sessionDate_bis"];
                ViewBag.Dauer2 = System.Web.HttpContext.Current.Session["sessionDate_von"];
                System.Web.HttpContext.Current.Session["sessionErgebnis"] = Convert.ToInt32(((ViewBag.Dauer - ViewBag.Dauer2).TotalDays) + 1);
                System.Web.HttpContext.Current.Session["sessionVon"] = Convert.ToString(date_von.ToString().Substring(0,10));
                System.Web.HttpContext.Current.Session["sessionBis"] = Convert.ToString(date_bis.ToString().Substring(0,10));
            }
            

            //ViewBag.dateVon = System.Web.HttpContext.Current.Session["sessionDate_von"];
            //ViewBag.dateBis = System.Web.HttpContext.Current.Session["sessionDate_bis"];

            //if (!string.IsNullOrEmpty(date_von) && !string.IsNullOrEmpty(date_bis))
            //{
            return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
        }
        /// <summary>
        /// POST: Home/Step3
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
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
        /// <summary>
        /// GET: Home/Step4
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// GET: Home/Step5
        /// </summary>
        /// <returns></returns>
        public ActionResult Step5()
        {
            return View();
        }
        /// <summary>
        /// GET: Home/Step6
        /// </summary>
        /// <returns></returns>
        public ActionResult Step6()
        {
            return View();
        }
        /// <summary>
        /// GET: Home/Impressum
        /// </summary>
        /// <returns></returns>
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