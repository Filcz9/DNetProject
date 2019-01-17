using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{
    [MetadataType(typeof(PeselMetadata))]
    public partial class Pesel
    {
        [Display(Name = "Pesel")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pesel jest wymagany")]
        [MinLength(11, ErrorMessage = "Pesel ma 11 liczb")]
        public string PeselNumber { get; set; }

        [Display(Name = "Data Urodzenia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data jest wymagana")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

    }

    public class PeselMetadata
    {
        


    }
}