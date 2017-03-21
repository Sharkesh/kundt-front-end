using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kundt_front_end.Models
{
    public class MaterialBurstMitBuchung
    {
        public List<tblAuto> autoListe { get; set; }
        public tblBuchung tBuchung { get; set; }
        public tblLogin tLogin { get; set; }
        public tblTyp Typ { get; set; }
        public tblMarke tMarke { get; set; }
        public tblKategorie tKategorie { get; set; }
        public tblAusstattung tAusstattung { get; set; }

    }
}