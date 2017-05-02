using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kundt_front_end.Models
{
    [MetadataType(typeof(tblPLZOrtMeta))]
    public partial class tblPLZOrt
    {

    }

    public class tblPLZOrtMeta
    {
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "PLZ:")]
        public string PLZ { get; set; }
        [Required]
        [Display(Name = "Ort:")]
        public string Ort { get; set; }
    }
}