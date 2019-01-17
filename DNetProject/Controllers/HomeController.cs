using DNetProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace DNetProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel homeModel = new HomeModel();
            IndexView(homeModel);

            return View(homeModel);
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

        public HomeModel IndexView(HomeModel homeModel)
        {
            List<Albums> albumList = new List<Albums>();
            List<Pictures> pictureList = new List<Pictures>();
            List<Albums> privateList = new List<Albums>();
            var currentUserId = 0;
            if (User.Identity.Name != "")
            {
                 currentUserId = UserId(User.Identity.Name);
            }
            else  currentUserId = 0;
            using (ProjektEntities context = new ProjektEntities())
            {
                var categoriesList = context.Albums.Where(a => a.visibility == 0).ToList<Albums>();
                var privatesList =
                        from a in context.Albums
                        where a.visibility == 1 && a.id_user == currentUserId
                        select a;

                foreach (Pictures pic in context.Pictures)
                {

                    pictureList.Add(pic);
                    ViewBag.pic = pic.img;
                }

                foreach(Albums priv in privatesList)
                {
                    privateList.Add(priv);
                }

                foreach (Albums category in categoriesList)
                {

                    albumList.Add(category);
                }

                pictureList.Reverse();     
                homeModel.pictureList = pictureList;
                homeModel.albumList = albumList;
                homeModel.privateList = privateList;
            }
            return homeModel;
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int picId)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var picture = context.Pictures.Where(a => a.id == picId).FirstOrDefault();
                var pical = context.PicturesAlbums.Where(a => a.pictures_id == picId).ToList();
                var comments = context.Comments.Where(a => a.id_picture == picId).ToList();

                foreach (Comments comment in comments)
                {
                    context.Comments.Remove(comment);
                }
                foreach (PicturesAlbums p in pical)
                {
                    context.PicturesAlbums.Remove(p);
                }
                context.Pictures.Remove(picture);
                context.SaveChanges();
            }
            HomeModel homeModel = new HomeModel();
            IndexView(homeModel);
            ViewBag.Deleted = "true";
            ViewBag.msg = "Zdjęcie zostało usunięte";
            return RedirectToAction("Index", "Home"); 
        }


    }
}