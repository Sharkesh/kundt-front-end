using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kundt_front_end.Models
{
    public class ModelStepClass
    {
       // public int StepID { get; set; }
        public DateTime date_von { get; set; }
        public DateTime date_bis { get; set; }

        public string date_von_string { get; set; }
        public string date_bis_string { get; set; }

        public int Mietdauer { get; set; }

        public List<tblAuto> autoListe { get; set; }
        public int gebuchtesAutoID { get; set; }
        public decimal Gesamtpreis { get; set; }

        public tblAuto gebuchtesAuto { get; set; }

        public bool hatRtVersicherung { get; set; }

        public int userID { get; set; }
        public tblKunde kunde { get; set; }

        public List<fCarAvailable_Result> carTable { get; set; }
        //public int IDAuto { get; set; }
        //public int PS { get; set; }
        //public int Tueren { get; set; }
        //public int Sitze { get; set; }
        //public decimal Mietpreis{ get; set; }
        //public  byte[] Autobild { get; set; }
        //public int MyProperty { get; set; }

        


    }
}
