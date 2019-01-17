using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DNetProject.Models
{
    [MetadataType(typeof(AlbumsMetadata))]
    public partial class Albums
    {
    }
    public class AlbumsMetadata
    {
        [Display(Name = "Nazwa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa wymagana")]
        public string album_name { get; set; }

    }
}