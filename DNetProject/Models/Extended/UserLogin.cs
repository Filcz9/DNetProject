using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{
    public class UserLogin
    {

            [Display(Name = "Username")]
            [Required(AllowEmptyStrings = false, ErrorMessage = "Username required")]
            public string username { get; set; }
            [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
            [DataType(DataType.Password)]
            public string password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        
    }
}