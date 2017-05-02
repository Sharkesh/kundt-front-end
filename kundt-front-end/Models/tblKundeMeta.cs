using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_front_end.Models
{
    [MetadataType(typeof(tblKundeMeta))]
    public partial class tblKunde
    {

    }

    public class tblKundeMeta
    {
        [Required]
        [Display(Name = "Vorname:")]
        public string Vorname { get; set; }
        [Required]
        [Display(Name = "Nachname:")]
        public string Nachname { get; set; }
        [Required]
        [Display(Name = "Straße:")]
        public string Strasse { get; set; }
        [Required]
        [Display(Name = "Telefon:")]
        public string Telefon { get; set; }
        [Required]
        [Display(Name = "Anrede:")]
        public string Anrede { get; set; }
        [Required]
        [Display(Name = "Geburtsdatum:")]
        public System.DateTime GebDatum { get; set; }
        [Required]
        [Display(Name = "Reisepassnummer:")]
        public string ReisepassNr { get; set; }
    }
}