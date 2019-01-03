﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        public ActionResult CRUDPanel()
        {
            return View();
        }
        public ActionResult UserList()
        {
            List<User> userList = new List<User>();
            using (ProjektEntities context = new ProjektEntities())
            {
                userList = context.Users.ToList();
            }

            foreach (User user in userList)
            {
                user.RoleName = GetRoleName(user.role_id);
            }

            return View(userList);
        }

        public ActionResult DeleteUser(string username)
        {
            List<User> userList = new List<User>();
            using (ProjektEntities context = new ProjektEntities())
            {
                var user = context.Users.Where(a => a.username == username).FirstOrDefault();
                context.Users.Remove(user);
                context.SaveChanges();
                userList = context.Users.ToList();
            }

            foreach (User user in userList)
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
                user.RoleCollection = context.Roles.ToList<Role>();

                return View(user);
            }
        }
        [HttpPost]
        public ActionResult EditUser(User userModel, int userId, string userPassword)
        {
            List<User> userList = new List<User>();
            try
            {


                using (ProjektEntities context = new ProjektEntities())
                {
                    var user = context.Users.Where(a => a.UserId == userId).FirstOrDefault();
                    user.Email = userModel.Email;
                    user.Username = userModel.Username;
                    user.RoleId = userModel.RoleId;
                    user.ConfirmPassword = user.Password;

                    context.SaveChanges();

                    userList = context.Users.ToList();
                }
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }

            foreach (User user in userList)
            {
                user.RoleName = GetRoleName(user.RoleId);
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

        public ActionResult Details(int id)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var app = context.ModeratorApplications.Where(a => a.ModeratorApplicationId == id).FirstOrDefault();
                if (app != null)
                    app.User.Username = UserName(app.UserId);

                return View(app);
            }
        }
        public ActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                foreach (var cat in context.Categories)
                {
                    if (category.CategoryName == cat.CategoryName)
                    {
                        ViewBag.error = "Kategoria o takiej nazwie już istnieje";
                        return View();
                    }
                }
                context.Categories.Add(category);
                context.SaveChanges();
            }

            ViewBag.cat = category.CategoryName;
            return View("CategoryAdded");
        }
    }
}
}