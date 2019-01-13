using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{

        [MetadataType(typeof(PictureMetadata))]
        public partial class Picture
    {
            public List<Album> AlbumCollection { get; set; }
            public DateTime InteractionDate { get; set; }
        }

        public partial class PictureMetadata
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

