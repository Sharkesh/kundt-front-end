using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kundt_front_end.Models;

namespace kundt_front_end.Controllers
{
    public class tblKundeController : Controller
    {
        private it22AutoverleihEntities db = new it22AutoverleihEntities();

        // GET: tblKunde
        public tblKunde Step5b(int? id)
        {
            tblAuto omg = new tblAuto();
            tblKunde kunde = db.tblKunde.Find(id);
            //ModelStepClass msc = new ModelStepClass();
            //msc.kunde = kunde;
            return kunde;
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
