using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
        public string RoleName { get; set; }
        public List<Role> RoleCollection { get; set; }
    }

    public class UserMetadata
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string username { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email jest wymagany")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]

        public string password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasło i potwiedzenie hasła nie zgadzają się")]
        public string ConfirmPassword { get; set; }

    }
}