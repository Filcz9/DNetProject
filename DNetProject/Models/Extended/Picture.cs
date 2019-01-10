using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNetProject.Models.Extended
{
    public class Picture
    {
        [MetadataType(typeof(GagMetadata))]
        public partial class Gag
        {
            public List<Album> AlbumCollection { get; set; }
            public DateTime InteractionDate { get; set; }
        }

        public partial class GagMetadata
        {
            [Display(Name = "Tytuł")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Tytuł jest wymagany")]
            [MinLength(2, ErrorMessage = "Tytył musi miec przynajmniej 2 znaki!")]
            public string Title { get; set; }

            [Display(Name = "Zdjęcie")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Zdjęcie jest wymagane")]
            public string Img { get; set; }

            [Display(Name = "Album")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Kategoria jest wymagana")]
            public string AlbumId { get; set; }
        }
    }
}
