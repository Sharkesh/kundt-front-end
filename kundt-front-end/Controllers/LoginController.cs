using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using kundt_front_end.Models;

namespace kundt_front_end.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        private static string conString = System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.Substring(System.Configuration.ConfigurationManager.ConnectionStrings["it22AutoverleihEntities"].ConnectionString.IndexOf("\"") + 1, 156);
        /// <summary>
        /// Connection
        /// </summary>
        public static SqlConnection con = new SqlConnection(conString);
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        /// <summary>
        /// GET: Login
        /// </summary>
        [RequireHttps]
        public ActionResult Index()
        {
            //Wenn bereits eingeloggt, dann zurück zur Startseite.
            if (System.Web.HttpContext.Current.Session["IDUser"] != null && (int)System.Web.HttpContext.Current.Session["IDUser"] > 0)
            {
                return RedirectToAction("Index", "Home", null);
            }
            //Schaut ob in URL ein "?ActivationCode=" argument ist.
            //Falls ja wird der Code darin auf richtigkeit überprüft
            if (Request.RawUrl.Contains("?ActivationCode="))
            {
                string activationCode = Request.RawUrl.Substring(Request.RawUrl.LastIndexOf('=') + 1);
                //Und der eintrag in der UserActivation Tabelle gelöscht. Damit ist der User Aktiviert.
                using (SqlCommand cmd = new SqlCommand("Validate_Activation"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AktivierungsCode", activationCode);
                        cmd.Connection = con;
                        con.Open();
                        //Gibt zurück wie viele Zeilen betroffen waren.
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                        //Wenn zumindest eine Spalte betroffen ist, war der Code richtig
                        if (rowsAffected == 1)
                        {
                            TempData["activationResult"] = ViewBag.activationResult = 0; //Activation successful
                            if (System.Web.HttpContext.Current.Session["msc"] != null)
                            {
                                TempData["msc"] = (ModelStepClass)System.Web.HttpContext.Current.Session["msc"];
                                System.Web.HttpContext.Current.Session["msc"] = null;
                            }
                        }
                        else
                        {
                            TempData["activationResult"] = ViewBag.activationResult = -1; //Invalid Activation code
                        }
                    }
                }
                return RedirectToAction("Step4", "Home", null);
            }
            //Anzeige der meldung dass die Email losgeschickt wurde
            if (TempData["registerResult"] != null)
            {
                ViewBag.registerResult = TempData["registerResult"];
            }
            return View();
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        [HttpPost]
        [RequireHttps]
        public ActionResult Index(string email, string password, ModelStepClass msc)
        {
            //PasswordConverter hashed das eingegebene Passwort im SHA256 Format
            password = Logic.Helpers.HashPassword(password);

            //Verbindung zur Datenbank per Connecton string wird definiert
            //Stored Procedure Validate_User wird definiert
            using (SqlCommand cmd = new SqlCommand("Validate_User"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Passwort", password);
                cmd.Connection = con;
                con.Open();
                //Aufruf der Prozedur
                //Rückabe Werte: -3: Acc Deaktiviert; -2: Acc nicht Aktivert; -1: Eingegebene Daten falsch; >0: Die zugehörige UserId
                ViewBag.loginResult = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (ViewBag.loginResult > 0)
            {
                using (SqlCommand cmd = new SqlCommand("Get_Role"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Connection = con;
                    con.Open();
                    ViewBag.role = Convert.ToChar(cmd.ExecuteScalar());
                    con.Close();
                }
                //Session Variablen werden je nach Rolle gesetzt.
                if (ViewBag.role == 'K')
                {
                    System.Web.HttpContext.Current.Session["IDUser"] = msc.userID = ViewBag.loginResult;
                    System.Web.HttpContext.Current.Session["Email"] = email;
                }
                else
                {
                    ViewBag.loginResult = -1;
                }

            }
            else
            {
                TempData.Add("loginResult", ViewBag.loginResult);
                //Bei fehler zurück zum Login
            }
            TempData["loginResult"] = ViewBag.loginResult;
            TempData["msc"] = msc;
            return Redirect(Convert.ToString(Request.UrlReferrer));
        }

        /// <summary>
        /// GET: Login/Registrierung
        /// </summary>
        [RequireHttps]
        public ActionResult Registrierung()
        {
            return RedirectToAction("Index", "Home", null);
        }

        /// <summary>
        /// POST: Login/Registrierung
        /// </summary>
        [HttpPost]
        [RequireHttps]
        public ActionResult Registrierung(string sex, string prename, string sirname, string adress, string city, string postcode, string telephone, string bDay, string bMonth, string bYear, string pass, string email, string password, ModelStepClass msc)
        {
            //PasswordConverter hashed das eingegebene Passwort im SHA256 Format
            password = Logic.Helpers.HashPassword(password);
            string GebDatum = $"{bDay}.{bMonth}.{bYear}";
            int userId = 0;
            //Stored Procedure Insert_User wird definiert
            using (SqlCommand cmd = new SqlCommand("Insert_User"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Passwort", password);
                    cmd.Parameters.AddWithValue("@Vorname", prename);
                    cmd.Parameters.AddWithValue("@Nachname", sirname);
                    cmd.Parameters.AddWithValue("@Strasse", adress);
                    cmd.Parameters.AddWithValue("@Telefon", telephone);
                    cmd.Parameters.AddWithValue("@Anrede", sex);
                    cmd.Parameters.AddWithValue("@GebDatum", GebDatum);
                    cmd.Parameters.AddWithValue("@ReisepassNr", pass);
                    cmd.Parameters.AddWithValue("@Ort", city);
                    cmd.Parameters.AddWithValue("@Plz", postcode);
                    cmd.Connection = con;
                    con.Open();
                    //Aufruf der Prozedur
                    //Rückgabe Werte: -2: PLZ/Ort ungültig; -1: Email schon vorhanden; >0: zugehörige UserId.
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            if (userId > 0)
            {
                Logic.Helpers.SendActivationEmail(userId, email);
                System.Web.HttpContext.Current.Session["msc"] = msc;
            }
            else
            {
                //Für die anzeige der Fehlermeldung
                ViewBag.registerResult = userId;
            }

            //Bei einem Redirect funktioniert ViewBag NICHT! Deshalb TempData.
            TempData["registerResult"] = userId;
            TempData["msc"] = msc;

            return Redirect(Convert.ToString(Request.UrlReferrer));
        }

        /// <summary>
        /// GET: Login/Logout/id
        /// </summary>
        [RequireHttps]
        public ActionResult Logout(int? id)
        {
            //Überprüft ob die mitgegebene ID stimmt
            if (System.Web.HttpContext.Current.Session["IDUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (id == (int)System.Web.HttpContext.Current.Session["IDUser"])
            {
                //Löscht die Session Variable
                System.Web.HttpContext.Current.Session["IDUser"] = null;
                System.Web.HttpContext.Current.Session["Email"] = null;
            }

            return RedirectToAction("Index", "Home");
        }

        [RequireHttps]
        public ActionResult Edit()
        {
            if (System.Web.HttpContext.Current.Session["IDUser"] != null)
            {
                tblKunde k = db.tblKunde.Find((int)System.Web.HttpContext.Current.Session["IDUser"]);
                if (k == null)
                {
                    return HttpNotFound();
                }
                return View(k);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        [HttpPost]
        [RequireHttps]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKunde,Vorname,Nachname,Strasse,Telefon,Anrede,ReisepassNr,GebDatum")] tblKunde k, string plz, string ort, string password, string newPassword)
        {
            if (ModelState.IsValid)
            {
                if (password != "" && newPassword != "")
                {
                    password = Logic.Helpers.HashPassword(password);
                    newPassword = Logic.Helpers.HashPassword(newPassword);
                }

                var result = db.pEditCustumer(k.IDKunde, k.Vorname, k.Nachname, k.Strasse, k.Telefon, k.Anrede, k.GebDatum, k.ReisepassNr, plz, ort, password, newPassword);
                int resultValue = result.SingleOrDefault().Value;

                if (resultValue < 0)
                {
                    TempData["editResult"] = resultValue;
                }
                return RedirectToAction("Edit");
            }
            return View(k);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

namespace kundt_front_end.Logic
{
    /// <summary>
    /// Sammlung aus Methoden die zur Hilfe bei Login verwendet werden können.
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        public static SqlConnection con = Controllers.LoginController.con;
        /// <summary>
        /// Wandelt den Mitgelieferten String in einen SHA256 Hash um.
        /// </summary>
        /// <param name="originalPassword">Ein String der in SHA256 gehashed werden soll.</param>
        /// <returns>Die Zeichenfolge in SHA256 Hash als String</returns>
        public static string HashPassword(string originalPassword)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            Byte[] encodedBytes = sha256.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        /// <summary>
        /// Legt in der UserActivation Tabelle einen Eintarg zur jeweiligen UserId an und generiert für die Aktivierungsmail eine GUID.
        /// Anschließend wird die Mail versandt.
        /// </summary>
        /// <param name="userId">Die zum Datensatz passende UserId.</param>
        /// <param name="email">Die zum Benutzer passende Email.</param>
        public static void SendActivationEmail(int userId, string email)
        {
            string activationCode = Guid.NewGuid().ToString();
            using (SqlCommand cmd = new SqlCommand("Insert_Activation"))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDLogin", userId);
                    cmd.Parameters.AddWithValue("@AktivierungsCode", activationCode);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            using (MailMessage mm = new MailMessage(/*"noreply@sharkesh.com"*/"test.sharkesh@gmail.com", email))
            {
                mm.Subject = "Account Aktivierung";
                //Nachrichten Text wird "zusammengebaut".
                string body = "<p>Hallo,";
                body += "<br /><br />Bitte klicke auf den folgenden Link um die Registrierung abzuschließen.";
                body += "<br /><a href='https://localhost:44322/Login?ActivationCode=" + activationCode + "'>Klicke hier um deinen Account zu Aktivieren.</a>";
                body += "<br /><br />Danke!</p>";
                //Nachrichten Text wird an das MailMessage Objekt gehängt.
                mm.Body = body;
                mm.IsBodyHtml = true;
                //Logindaten für den SmtpClient weiter unten.
                NetworkCredential NetworkCred = new NetworkCredential(/*"noreply"*/"test.sharkesh@gmail.com", /*"~S[%a(1<`(eN"*/"123user!");
                //Verbindungs Variablen werden gesetzt.
                SmtpClient smtp = new SmtpClient()
                {
                    Host = "smtp.gmail.com"/*"cloud.sharkesh.com"*/,
                    EnableSsl = true,
                    UseDefaultCredentials = true,
                    Credentials = NetworkCred,
                    Port = 587
                };
                smtp.Send(mm);
            }
        }
    }
}
