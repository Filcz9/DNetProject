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

        public ActionResult IndexCategory(string categoryName)
        {
            HomeModel homeModel = new HomeModel();
            IndexView(homeModel);
            List<Albums> albumList = new List<Albums>();
            List<Pictures> gagsList = new List<Pictures>();
            List<Comments> commentList = new List<Comments>();
            List<Albums> privateList = new List<Albums>();
            var currentUserId = 0;
            if (User.Identity.Name != "")
            {
                currentUserId = UserId(User.Identity.Name);
            }
            else currentUserId = 0;
            using (ProjektEntities context = new ProjektEntities())
            {
                var categoriesList = context.Albums.Where(a => a.visibility == 0).ToList<Albums>();
                var privates =
                        from a in context.Albums
                        where a.visibility == 1 && a.id_user == currentUserId
                        select a;

                var gags = context.Pictures.ToList();
                foreach (var priv in privates)
                {
                    privateList.Add(priv);
                }

                foreach (Albums category in categoriesList)
                {

                    albumList.Add(category);
                }
                //var categoriesList = context.Categories.ToList();
                foreach (Pictures gag in gags)
                {
                    //var name = context.Categories.Where(a => a.CategoryId == gag.CategoryId).FirstOrDefault();
                    //gag.Category.CategoryName = name.CategoryName;
                    gagsList.Add(gag);
                    ViewBag.pic = gag.img;
                }
                //foreach (Category category in categoriesList)
                //{
                //    categoryList.Add(category);
                //}
                homeModel.privateList = privateList;
                homeModel.pictureComments = commentList;
                homeModel.pictureList = gagsList;
                homeModel.albumList = albumList;
            }
            gagsList.Reverse();
            return View("Index", homeModel);
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
            List<Pictures> gagsList = new List<Pictures>();
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
                //var privatesList = context.Albums.Where(a => a.visibility == 1).ToList<Albums>();
                var privatesList =
                        from a in context.Albums
                        where a.visibility == 1 && a.id_user == currentUserId
                        select a;

                foreach (Pictures gag in context.Pictures)
                {
                    //var name = context.Albums.Where(a => a.id == gag.).FirstOrDefault();
                    //gag.Category.CategoryName = name.CategoryName;
                    gagsList.Add(gag);
                    ViewBag.pic = gag.img;
                }

                //foreach (GagsUser gag in context.GagsUsers)
                //{
                //    var checkUpvote = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name).FirstOrDefault();

                //    // ViewBag.pic = gag.Picture;
                //}
                foreach(Albums priv in privatesList)
                {
                    privateList.Add(priv);
                }

                foreach (Albums category in categoriesList)
                {

                    albumList.Add(category);
                }

                //foreach (Pictures gag in context.Pictures)
                //{
                //    if (gag.Points > topGag.Points)
                //        topGag = gag;
                //}
                gagsList.Reverse();
                //homeModel.topGag = topGag;
                homeModel.pictureList = gagsList;
                homeModel.albumList = albumList;
                homeModel.privateList = privateList;
                //homeModel.gagsUserList = gagsUsersList;
            }
            return homeModel;
        }
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult DeleteGag(int gagId)
        {
            using (ProjektEntities context = new ProjektEntities())
            {
                var gag = context.Pictures.Where(a => a.id == gagId).FirstOrDefault();
                var gagPoints = context.PicturesAlbums.Where(a => a.pictures_id == gagId).ToList();
                foreach (PicturesAlbums g in gagPoints)
                {
                    context.PicturesAlbums.Remove(g);
                }
                context.Pictures.Remove(gag);
                context.SaveChanges();
            }
            HomeModel homeModel = new HomeModel();
            IndexView(homeModel);
            ViewBag.Deleted = "true";
            ViewBag.msg = "Gag został usunięty i nikt już nie będzie w stanie go zobaczyć!";
            return View(homeModel);
        }


    }
}