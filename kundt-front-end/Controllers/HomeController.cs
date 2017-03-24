using kundt_front_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
            ModelStepClass msc = new ModelStepClass();
            // msc.StepID = 1;
            msc.userID = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);
            return View(msc);
        }
        /// <summary>
        /// POST: Home/Step2
        /// </summary>
        /// <param name="date_von"></param>
        /// <param name="date_bis"></param>
        /// <returns></returns>
        //public ActionResult Step2(DateTime? date_von, DateTime? date_bis)
        [HttpPost]
        public ActionResult Step2(ModelStepClass msc)
        {
            // anstatt von hidden Fields, exestierende Daten mittels tempdata mitschleifen

            //Sobald sproc steht, nur entsprechende Autos laden
            var autoListe = db.fCarAvailable(msc.date_von_string, msc.date_bis_string).ToList(); // später auf Eager Loading ändern ?
            msc.carTable = autoListe.ToList();

            //dt string in dt objekt umwandeln
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            //

            msc.Mietdauer = Convert.ToInt32((msc.date_bis - msc.date_von).TotalDays) + 1;
            // msc.StepID = 2;
            //ViewBag.date_von = date_von;
            //ViewBag.date_bis = date_bis;

            //ModelStepClass ViewModel = new ModelStepClass();
            //ViewModel.autoListe = msc.autoListe;
            //ViewModel.date_bis = msc.date_bis;
            //ViewModel.date_von = msc.date_von;
            //ViewModel.StepID = msc.StepID;
            return View(msc);
        }
        public ActionResult Step3(/*int? id,*/ModelStepClass msc/*, DateTime? date_von, DateTime? date_bis*//*, int Ergebnis*/) //Get Object with ID
        {
            //msc.StepID = 3;

            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);

            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            return View(/*afec.Step3b(id)*/msc);
        }
        public ActionResult Step4(ModelStepClass msc) //Get Object with ID
        {
            //Wenn eingeloggt dann diesen Step überspringen
            if (System.Web.HttpContext.Current.Session["IDUser"] != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                System.Web.HttpContext.Current.Session["IDAuto"] = (int)msc.gebuchtesAutoID;

                TempData["msc"] = msc;
                return RedirectToAction("Step5");
            }

            //Deprecated
            if (TempData["registerResult"] != null)
            {
                ViewBag.registerResult = TempData["registerResult"];
            }
            if (TempData["activationResult"] != null)
            {
                ViewBag.activationResult = TempData["activationResult"];
            }
            return View(msc);
        }
        public ActionResult Step5()
        {
            ModelStepClass msc = (ModelStepClass)TempData["msc"];
            //msc.StepID = 5;

            msc.kunde = db.tblKunde.Find(msc.userID);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);

            //if (System.Web.HttpContext.Current.Session["IDAuto"] != null)
            //{
            //    id = (int)System.Web.HttpContext.Current.Session["IDAuto"];
            //}
            //tblKundeController kuco = new tblKundeController();
            return View(msc); //Get Object with ID
        }
        public ActionResult Step6(ModelStepClass msc)
        {
            //MSC enthaelt keinen Gesamtpreis, ist aber auch nicht wichtig zum Erstellen der Buchung
            //Waere natuerlich schoen, wenn man noch herausfindet warum

            ////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht
            db.pBuchungAnlegen(msc.userID, msc.gebuchtesAutoID, msc.date_von_string, msc.date_bis_string, false, false);
            ////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht

            //PDF erstellen

            //Email mit pdf verschicken

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
