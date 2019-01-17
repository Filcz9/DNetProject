using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNetProject.Models;
using System.Data.Entity.Validation;

namespace DNetProject.Controllers
{
    public class VerifyController : Controller
    {
        // GET: Verify
        [HttpGet]
        public ActionResult VerifyAge()
        {
            var currentUserId = UserId(User.Identity.Name);
            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.id == currentUserId).FirstOrDefault();
                user.RoleCollection = context.Roles.ToList<Roles>();

                return View(user);
            }
        }
        [HttpPost]
        public ActionResult VerifyAge(string PeselNumber, DateTime BirthDate)
        {
            var currentUserId = UserId(User.Identity.Name);
            List<Users> userList = new List<Users>();
            IPeselValidator peselValidator = new PeselValidator();
            bool checkPesel = peselValidator.ValidatePesel(PeselNumber);
            bool checkPeselAndAge = false;
            if (checkPesel == false)
            {
                ViewBag.peselError = "Podany pesel jest niepoprawny";
                return View();
            }

            if (peselValidator.ValidateAge(BirthDate))
            {
                checkPeselAndAge = peselValidator.ValidatePeselAndBirthDate(PeselNumber, BirthDate);
            }

            if (checkPeselAndAge)
            {
    
                    using (ProjektEntities context = new ProjektEntities())
                    {
                        var user = context.Users.Where(a => a.id == currentUserId).FirstOrDefault();
                         user.email = user.email;
                         user.username = user.username;
                         user.ConfirmPassword = user.password;
                         user.role_id = 3;
                        context.SaveChanges();
                    }
                


                return View("Index");
            }
            else
            {
                ViewBag.peselError = "Podany pesel i data nie zgadzają się ze sobą";
                return View();
            }
           

        }
        public int UserId(string name)
        {

            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.username == User.Identity.Name).FirstOrDefault();
                var userId = user.id;
                return userId;
            }
        }
    }
}