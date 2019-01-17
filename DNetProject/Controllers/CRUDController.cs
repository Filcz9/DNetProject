using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DNetProject.Models;
using System.Data.Entity.Validation;

namespace DNetProject.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult UserList()
        {
            List<Users> userList = new List<Users>();
            using (ProjektEntities context = new ProjektEntities())
            {
                userList = context.Users.ToList();
            }

            foreach (Users user in userList)
            {
                user.RoleName = GetRoleName(user.role_id);
            }

            return View(userList);
        }

        public ActionResult DeleteUser(string username)
        {
            List<Users> userList = new List<Users>();
            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.username == username).FirstOrDefault();
                var comment = context.Comments.Where(a => a.id_user == user.id).ToList();
                context.Users.Remove(user);


                foreach(Comments com in comment)
                {
                    context.Comments.Remove(com);
                }
                context.SaveChanges();
                userList = context.Users.ToList();
            }

            foreach (Users user in userList)
            {
                user.RoleName = GetRoleName(user.role_id);
            }

            return View("UserList", userList);
        }
        [HttpGet]
        public ActionResult EditUser(string username)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.username == username).FirstOrDefault();
                user.RoleCollection = context.Roles.ToList<Roles>();

                return View(user);
            }
        }
        [HttpPost]
        public ActionResult EditUser(Users userModel, int userId, string userPassword)
        {
            List<Users> userList = new List<Users>();

                using (ProjektEntities context = new ProjektEntities())
                {
                    var user = context.Users.Where(a => a.id == userId).FirstOrDefault();
                    user.email = userModel.email;
                    user.username = userModel.username;
                    user.role_id = userModel.role_id;
                    user.ConfirmPassword = user.password;

                    context.SaveChanges();

                    userList = context.Users.ToList();
                }

            foreach (Users user in userList)
            {
                user.RoleName = GetRoleName(user.role_id);
            }

            return View("UserList", userList);

        }

        

        public string GetRoleName(int? roleId)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var role = context.Roles.Where(a => a.id == roleId).FirstOrDefault();
                string roleName = role.name;
                return roleName;
            }
        }
        public string UserName(int? userId)
        {

            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.id== userId).FirstOrDefault();
                var userName = user.username;
                return userName;
            }
        }


        public ActionResult AddAlbum()
        {

            return View();
        }
        public ActionResult AddPrivateAlbum()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddAlbum(Albums album)
        {
            if (album.album_name == null)
            {
                return View();
            }
            var currentUserId = UserId(User.Identity.Name);
            using (ProjektEntities context = new ProjektEntities())
            {
                album.visibility = 0;
                album.id_user = currentUserId;
                context.Albums.Add(album);
                context.SaveChanges();
            }

            ViewBag.cat = album.album_name;
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult AddPrivateAlbum(Albums album)
        {
            if (album.album_name == null)
            {
                return View();
            }
            else
            {
                var currentUserId = UserId(User.Identity.Name);
                using (ProjektEntities context = new ProjektEntities())
                {

                    album.visibility = 1;
                    album.id_user = currentUserId;
                    context.Albums.Add(album);
                    context.SaveChanges();
                }

                ViewBag.cat = album.album_name;
                return RedirectToAction("Index", "Home");
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

