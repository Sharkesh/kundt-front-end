using kundt_front_end.Models;
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
using System.Net.Mail;
using System.Net;

namespace kundt_front_end.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Datenbank Objekt für EF
        /// </summary>
        private it22AutoverleihEntities db = new it22AutoverleihEntities();
        /// <summary>
        /// GET: Home/Index
        /// </summary>
        [RequireHttps]
        public ActionResult Index()
        {
            // Falls noch ein TempData vorhanden, wird es gelöscht
            TempData["msc"] = null;

            ModelStepClass msc = new ModelStepClass();

            msc.notAgain = false;

            msc.userID = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);
            msc.tec = db.tblEyecatcher.ToList();
            return View(msc);
        }
        /// <summary>
        /// POST: Home/Step2
        /// </summary>
        [HttpPost]
        [RequireHttps]
        public ActionResult Step2(ModelStepClass msc)
        {
            msc.userID = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);

            if (msc.date_von_string != null && msc.date_bis_string != null)
            {
                msc.date_von = Convert.ToDateTime(msc.date_von_string);
                msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            }
            else
            {
                return RedirectToAction("index");
            }

            //procs und Validierung der übergebenen Daten
            string sitze = null;
            string klasse = null;

            sitze = msc.SitzanzahlFilter;
            klasse = msc.KlasseFilter;


            var autoListe = db.pCarAvailableFinal(msc.date_von_string, msc.date_bis_string, klasse).ToList(); // später auf Eager Loading ändern ?
            msc.carTableFilter = autoListe.ToList(); //Filtert verf. Autos mit Filteroptionen


            msc.Mietdauer = Convert.ToInt32((msc.date_bis - msc.date_von).TotalDays) + 1;

            return View(msc);
        }

        /// <summary>
        /// POST: Home/Step3
        /// </summary>
        [RequireHttps]
        public ActionResult Step3(ModelStepClass msc) //Get Object with ID
        {
            msc.userID = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            return View(msc);
        }

        /// <summary>
        /// GET/POST: Home/Step4
        /// </summary>
        [RequireHttps]
        public ActionResult Step4(ModelStepClass msc)
        {
            if (TempData["msc"] != null)
            {
                msc = (ModelStepClass)TempData["msc"];
            }

            msc.userID = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);

            //Wenn eingeloggt dann diesen Step überspringen
            if (System.Web.HttpContext.Current.Session["IDUser"] != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                TempData["msc"] = msc;
                return RedirectToAction("Step5");
            }
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.kunde = db.tblKunde.Find(Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]));
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

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

        /// <summary>
        /// GET: Home/Step5
        /// </summary>
        [RequireHttps]
        public ActionResult Step5(ModelStepClass msc)
        {
            if (TempData["msc"] != null)
            {
                msc = (ModelStepClass)TempData["msc"];
            }            
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.kunde = db.tblKunde.Find(Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]));
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;
            return View(msc); //Get Object with ID
        }

        [RequireHttps]
        public ActionResult Print(ModelStepClass msc)
        {
            msc.kunde = db.tblKunde.Find(Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]));
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            return new ViewAsPdf("ViewPDF", msc);
        }

        [RequireHttps]
        public ActionResult SendMail_Again(ModelStepClass msc)
        {
            msc.kunde = db.tblKunde.Find(Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]));
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.date_bis = Convert.ToDateTime(msc.date_bis_string);
            msc.date_von = Convert.ToDateTime(msc.date_von_string);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;

            string path = HttpContext.ApplicationInstance.Server.MapPath(String.Format("~/App_Data/PDF/{0}.pdf", msc.IDBuchung));

            using (MailMessage mm = new MailMessage("test.sharkesh@gmail.com", msc.kunde.tblLogin.Email))
            {
                mm.Subject = "Buchungsbestätigung";

                string body = "<p>Vielen Dank für ihre Buchung,";
                body += "<br /><br />Ihre Buchungsbestätigung befindet sich im Anhang als PDF.";
                body += "<br /><br />Ihr Kundt-Autoverleih</p>";
                //Nachrichten Text wird an das MailMessage Objekt gehängt.
                mm.Body = body;
                mm.IsBodyHtml = true;
                Attachment Anhnag = new Attachment(path);
                mm.Attachments.Add(Anhnag);


                NetworkCredential NetworkCred = new NetworkCredential("test.sharkesh@gmail.com", "123user!");

                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = true,
                    Credentials = NetworkCred,
                    Port = 587
                };
                smtp.Send(mm);
            }
            TempData["sendMSG"] = true;

            return RedirectToAction("Step6",msc);
        }

        /// <summary>
        /// GET: Home/Step6
        /// </summary>
        [RequireHttps]
        public ActionResult Step6(ModelStepClass msc)
        {

            msc.kunde = db.tblKunde.Find(Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]));
            msc.gebuchtesAuto = db.tblAuto.Find(msc.gebuchtesAutoID);
            msc.Gesamtpreis = msc.gebuchtesAuto.MietPreis * msc.Mietdauer;


            if (msc.notAgain == false)
            {
                int IDBuchung;

                string constring = System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.Substring(System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.IndexOf("\"") + 1, 156);

                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("pBuchungAnlegen", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter KundeID = new SqlParameter("@varIDKunde", SqlDbType.Int);
                        KundeID.Value = Convert.ToInt32(System.Web.HttpContext.Current.Session["IDUser"]);
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

                msc.IDBuchung = IDBuchung;
                msc.notAgain = true;
                TempData["msc"] = msc;

                var pdf = new ViewAsPdf("ViewPDF", msc);
                var file = pdf.BuildPdf(ControllerContext);
                string path = HttpContext.ApplicationInstance.Server.MapPath(String.Format("~/App_Data/PDF/{0}.pdf", IDBuchung));
                var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                fileStream.Write(file, 0, file.Length);
                fileStream.Close();

                using (MailMessage mm = new MailMessage("test.sharkesh@gmail.com", msc.kunde.tblLogin.Email))
                {
                    mm.Subject = "Buchungsbestätigung";

                    string body = "<p>Vielen Dank für ihre Buchung,";
                    body += "<br /><br />Ihre Buchungsbestätigung befindet sich im Anhang als PDF.";
                    body += "<br /><br />Ihr Kundt-Autoverleih</p>";
                    //Nachrichten Text wird an das MailMessage Objekt gehängt.
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    Attachment Anhnag = new Attachment(path);
                    mm.Attachments.Add(Anhnag);


                    NetworkCredential NetworkCred = new NetworkCredential("test.sharkesh@gmail.com", "123user!");

                    SmtpClient smtp = new SmtpClient()
                    {
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        UseDefaultCredentials = true,
                        Credentials = NetworkCred,
                        Port = 587
                    };
                    smtp.Send(mm);
                }
            }
			else
			{
				TempData["send"] = true;
			}

            return View(msc);
        }

        /// <summary>
        /// GET: Home/Impressum
        /// </summary>
        [RequireHttps]
        public ActionResult Impressum()
        {
            return View();
        }

        /// <summary>
        /// GET: Home/AGB
        /// </summary>
        [RequireHttps]
        public ActionResult AGB()
        {
            return View();
        }
    }
}
