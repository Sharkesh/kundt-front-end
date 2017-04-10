﻿using kundt_front_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rotativa;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace kundt_front_end.Controllers
{
    public class HomeController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();
        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns></returns>
        [RequireHttps]
        public ActionResult Index()
        {
            ModelStepClass msc = new ModelStepClass();
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
        [RequireHttps]
        public ActionResult Step2(ModelStepClass msc)
        {
            // anstatt von hidden Fields, exestierende Daten mittels tempdata mitschleifen
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            //procs und Validierung der übergebenen Daten
            string sitze = null;
            string klasse = null;

            sitze = msc.SitzanzahlFilter;
            klasse = msc.KlasseFilter;


            var autoListe = db.pCarAvailableFinal(msc.date_von_string, msc.date_bis_string, klasse, sitze).ToList(); // später auf Eager Loading ändern ?
            msc.carTableFilter = autoListe.ToList(); //Filtert verf. Autos mit Filteroptionen


            msc.Mietdauer = Convert.ToInt32((msc.date_bis - msc.date_von).TotalDays) + 1;

            return View(msc);
        }

        [RequireHttps]
        public ActionResult Step3(ModelStepClass msc) //Get Object with ID
        {
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);

            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            return View(msc);
        }

        [RequireHttps]
        public ActionResult Step4(ModelStepClass msc) //Get Object with ID
        {



            if (TempData["msc"] != null)
            {
                msc = (ModelStepClass)TempData["msc"];
            }
            if (System.Web.HttpContext.Current.Session["msc"] != null)
            {
                msc = (ModelStepClass)System.Web.HttpContext.Current.Session["msc"];
            }

            //Wenn eingeloggt dann diesen Step überspringen
            if (System.Web.HttpContext.Current.Session["IDUser"] != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                System.Web.HttpContext.Current.Session["IDAuto"] = (int)msc.gebuchtesAutoID;

                TempData["msc"] = msc;
                return RedirectToAction("Step5");
            }

            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.kunde = db.tblKunde.Find(msc.userID);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;


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
        [RequireHttps]
        public ActionResult Step5(ModelStepClass msc)
        {
            if (TempData["msc"] != null)
            {
                msc = (ModelStepClass)TempData["msc"];
            }

            msc.kunde = db.tblKunde.Find(msc.userID);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);

            return View(msc); //Get Object with ID
        }

        [RequireHttps]
        public ActionResult Print()
        {
            ModelStepClass msc = (ModelStepClass)TempData["msc"];

            TempData["msc"] = msc;


            msc.kunde = db.tblKunde.Find(msc.userID);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            return new ViewAsPdf("ViewPDF", msc);

        }

        [RequireHttps]
        public ActionResult Step6(ModelStepClass msc)
        {

            msc.kunde = db.tblKunde.Find(msc.userID);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);            


            TempData["msc"] = msc;
            //MSC enthaelt keinen Gesamtpreis, ist aber auch nicht wichtig zum Erstellen der Buchung
            //Waere natuerlich schoen, wenn man noch herausfindet warum

            ////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht
            int IDBuchung;

            string constring = "Data Source=sql1;Initial Catalog=it22Autoverleih;Persist Security Info=True;User ID=it22;Password=123user!";
            //string constring = ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("pBuchungAnlegen", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter KundeID = new SqlParameter("@varIDKunde",SqlDbType.Int);
                    KundeID.Value = msc.userID;
                    KundeID.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(KundeID);

                    SqlParameter AutoID = new SqlParameter("@varIDAuto", SqlDbType.Int);
                    AutoID.Value = msc.gebuchtesAutoID;
                    AutoID.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(AutoID);

                    SqlParameter BuchungVon = new SqlParameter("@varBuchungVon", SqlDbType.VarChar);
                    BuchungVon.Value = msc.date_von_string;
                    BuchungVon.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(BuchungVon);

                    SqlParameter BuchungBis = new SqlParameter("@varBuchungBis", SqlDbType.VarChar);
                    BuchungBis.Value = msc.date_bis_string;
                    BuchungBis.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(BuchungBis);

                    SqlParameter Versicherung = new SqlParameter("@varVersicherung", SqlDbType.Bit);
                    Versicherung.Value = msc.HatRtVersicherung;
                    Versicherung.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(Versicherung);

                    SqlParameter Storno = new SqlParameter("@varStorno", SqlDbType.Bit);
                    Storno.Value = 0;
                    Storno.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(Storno);

                    SqlParameter outBuchungID = new SqlParameter("@IDBuchung", SqlDbType.Int);
                    outBuchungID.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outBuchungID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    IDBuchung = (int)cmd.Parameters["@IDBuchung"].Value;
                }
            }


            var pdf = new ViewAsPdf("ViewPDF", msc);
            var file = pdf.BuildPdf(ControllerContext);
            string path = HttpContext.ApplicationInstance.Server.MapPath(String.Format("~/Content/PDF/{0}.pdf",IDBuchung));
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            fileStream.Write(file, 0, file.Length);
            fileStream.Close();

            //db.pBuchungAnlegen(msc.userID, msc.gebuchtesAutoID, msc.date_von_string, msc.date_bis_string, false, false);


            //MSC enthaelt keinen Gesamtpreis, ist aber auch nicht wichtig zum Erstellen der Buchung
            //Waere natuerlich schoen, wenn man noch herausfindet warum

            ////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht
            //db.pBuchungAnlegen(msc.userID, msc.gebuchtesAutoID, msc.date_von_string, msc.date_bis_string, msc.HatRtVersicherung, false);

            ////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht////Versicherung funzt noch nicht

            //PDF erstellen

            //Email mit pdf verschicken

            return View();
        }
        [RequireHttps]
        public ActionResult Impressum()
        {
            return View();
        }
        [RequireHttps]
        public ActionResult AGB()
        {
            return View();
        }
    }
}
