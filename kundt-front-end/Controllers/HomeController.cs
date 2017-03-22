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
        public ActionResult Step2(string date_von, string date_bis)
        {

            //ViewBag.date_von = date_von;
            //ViewBag.date_bis = date_bis;
            if (!string.IsNullOrEmpty(date_von) && !string.IsNullOrEmpty(date_bis))
            {
                //alte Variante mit Date to String Convert
                System.Web.HttpContext.Current.Session["sessionDate_von"] = Convert.ToDateTime(date_von);
                System.Web.HttpContext.Current.Session["sessionDate_bis"] = Convert.ToDateTime(date_bis);
                //ViewBag.Dauer = System.Web.HttpContext.Current.Session["sessionDate_bis"];
                //ViewBag.Dauer2 = System.Web.HttpContext.Current.Session["sessionDate_von"];
                //System.Web.HttpContext.Current.Session["sessionErgebnis"] = Convert.ToInt32(((ViewBag.Dauer - ViewBag.Dauer2).TotalDays) + 1);
                //System.Web.HttpContext.Current.Session["sessionVon"] = Convert.ToString(date_von.ToString().Substring(0, 10));
                //System.Web.HttpContext.Current.Session["sessionBis"] = Convert.ToString(date_bis.ToString().Substring(0, 10));
                
                var date1 = Convert.ToDateTime(date_bis);
                var date2 = Convert.ToDateTime(date_von);
                System.Web.HttpContext.Current.Session["sessionDate_dauer"] = (date1 - date2).Days;


            }
            return View();
        }


        public ActionResult Step3(int? id) //Get Object with ID
        {
            //ViewBag.dateVon = System.Web.HttpContext.Current.Session["sessionDate_von"];
            //ViewBag.dateBis = System.Web.HttpContext.Current.Session["sessionDate_bis"];

            tblAutoFrontEndController afec = new tblAutoFrontEndController();
            return View(afec.Step3b(id));
        }

        public ActionResult Step4(int? id) //Get Object with ID
        {
            //Wenn eingeloggt dann diesen Step überspringen
            if (System.Web.HttpContext.Current.Session["IDUser"] != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                System.Web.HttpContext.Current.Session["IDAuto"] = (int)id;
                return RedirectToAction("Step5");
            }
            if (TempData["registerResult"] != null)
            {
                ViewBag.registerResult = TempData["registerResult"];
            }
            if (TempData["activationResult"] != null)
            {
                ViewBag.activationResult = TempData["activationResult"];
            }

            tblAutoFrontEndController afec = new tblAutoFrontEndController();
            if (afec.Step3b(id) != null)
            {
                return View(afec.Step3b(id));
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }
        }

        
        public ActionResult Step5(int? id)
        {
            if (System.Web.HttpContext.Current.Session["IDAuto"] != null)
            {
                id = (int)System.Web.HttpContext.Current.Session["IDAuto"];
            }
            tblKundeController kuco = new tblKundeController();
            return View(kuco.Step5b(id)); //Get Object with ID
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