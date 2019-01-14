using DNetProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace IGags.Controllers
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
            List<Albums> categoryList = new List<Albums>();
            List<Pictures> gagsList = new List<Pictures>();
            using (ProjektEntities context = new ProjektEntities())
            {
                var gags = context.Pictures.ToList();
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

                homeModel.pictureList = gagsList;
                //homeModel.categoryList = categoryList;
            }
            gagsList.Reverse();
            return View("Index", homeModel);
        }

        //public ActionResult Upvote(int id, string categoryName, string url)
        //{
        //    var CurrentUserId = UserId(User.Identity.Name);
        //    if (Request.IsAuthenticated)
        //    {

        //        using (ProjektEntities context = new ProjektEntities())
        //        {
        //            var checkUpvote = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name && a.GagId == id && a.Vote == 1).FirstOrDefault();
        //            var checkDownvote = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name && a.GagId == id && a.Vote == 0).FirstOrDefault();
        //            if (checkUpvote == null)
        //            {
        //                var gagUpvote = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                if (checkDownvote != null)
        //                {
        //                    gagUpvote.Points += 2;
        //                    context.GagsUsers.Remove(checkDownvote);
        //                }
        //                else
        //                {
        //                    gagUpvote.Points += 1;
        //                }

        //                GagsUser gUser = new GagsUser()
        //                {
        //                    UserId = UserId(User.Identity.Name),
        //                    GagId = id,
        //                    Vote = 1,
        //                    InteractionDate = DateTime.Now
        //                };
        //                context.GagsUsers.Add(gUser);


        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                var gagUpvote = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                gagUpvote.Points -= 1;
        //                var deleteUpvote = context.GagsUsers.Where(a => a.GagId == id && a.UserId == CurrentUserId && a.Vote == 1).FirstOrDefault();

        //                context.GagsUsers.Remove(deleteUpvote);
        //                context.SaveChanges();
        //            }

        //        }
        //    }
        //    HomeModel homeModel = new HomeModel();
        //    IndexView(homeModel);
        //    if (url != null)
        //        //if (Regex.Matches(url, categoryName).Count > 1 && url != "/" || !url.Contains("Home") && url != "/")
        //        if (url.Contains(categoryName))
        //        {
        //            List<Category> categoryList = new List<Category>();
        //            List<Gag> gagsList = new List<Gag>();
        //            using (ProjektEntities context = new ProjektEntities())
        //            {
        //                var gags = context.Gags.Where(a => a.Category.CategoryName == categoryName).ToList();
        //                if (gags.Count() > 0)
        //                {


        //                    var categoriesList = context.Categories.ToList();
        //                    foreach (Gag gag in gags)
        //                    {
        //                        var name = context.Categories.Where(a => a.CategoryId == gag.CategoryId).FirstOrDefault();
        //                        gag.Category.CategoryName = name.CategoryName;
        //                        gagsList.Add(gag);
        //                        ViewBag.pic = gag.Picture;
        //                    }
        //                    foreach (Category category in categoriesList)
        //                    {
        //                        categoryList.Add(category);
        //                    }

        //                    var gagObject = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                    var name2 = context.Categories.Where(a => a.CategoryId == gagObject.CategoryId).FirstOrDefault();
        //                    gagObject.Category.CategoryName = name2.CategoryName;
        //                    homeModel.gag = gagObject;
        //                    gagsList.Reverse();
        //                    homeModel.gagList = gagsList;
        //                    homeModel.categoryList = categoryList;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (ProjektEntities context = new ProjektEntities())
        //            {
        //                var gagObject = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                var name = context.Categories.Where(a => a.CategoryId == gagObject.CategoryId).FirstOrDefault();
        //                gagObject.Category.CategoryName = name.CategoryName;
        //                homeModel.gag = gagObject;
        //            }
        //        }


        //    return PartialView("GagObject", homeModel);
        //}

        //public ActionResult Downvote(int id, string categoryName, string url)
        //{
        //    var CurrentUserId = UserId(User.Identity.Name);
        //    if (Request.IsAuthenticated)
        //    {

        //        using (ProjektEntities context = new ProjektEntities())
        //        {
        //            var checkDownvote = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name && a.GagId == id && a.Vote == 0).FirstOrDefault();
        //            var checkUpvote = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name && a.GagId == id && a.Vote == 1).FirstOrDefault();
        //            if (checkDownvote == null)
        //            {
        //                var gagDownvote = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                if (checkUpvote != null)
        //                {
        //                    gagDownvote.Points -= 2;
        //                    context.GagsUsers.Remove(checkUpvote);
        //                }
        //                else
        //                {
        //                    gagDownvote.Points -= 1;
        //                }

        //                GagsUser gUser = new GagsUser()
        //                {
        //                    UserId = UserId(User.Identity.Name),
        //                    GagId = id,
        //                    Vote = 0,
        //                    InteractionDate = DateTime.Now
        //                };
        //                context.GagsUsers.Add(gUser);

        //                context.SaveChanges();
        //            }
        //            else
        //            {
        //                var gagDownvote = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                gagDownvote.Points += 1;

        //                var deleteDownvote = context.GagsUsers.Where(a => a.GagId == id && a.UserId == CurrentUserId && a.Vote == 0).FirstOrDefault();

        //                context.GagsUsers.Remove(deleteDownvote);

        //                context.SaveChanges();
        //            }

        //        }
        //    }
        //    HomeModel homeModel = new HomeModel();
        //    IndexView(homeModel);
        //    if (url != null)
        //        //if (Regex.Matches(url, categoryName).Count > 1 && url != "/" ||   !url.Contains("Home") && url != "/")
        //        if (url.Contains(categoryName))
        //        {
        //            List<Category> categoryList = new List<Category>();
        //            List<Gag> gagsList = new List<Gag>();
        //            using (ProjektEntities context = new ProjektEntities())
        //            {
        //                var gags = context.Gags.Where(a => a.Category.CategoryName == categoryName).ToList();
        //                if (gags.Count() > 0)
        //                {


        //                    var categoriesList = context.Categories.ToList();
        //                    foreach (Gag gag in gags)
        //                    {
        //                        var name = context.Categories.Where(a => a.CategoryId == gag.CategoryId).FirstOrDefault();
        //                        gag.Category.CategoryName = name.CategoryName;
        //                        gagsList.Add(gag);
        //                        ViewBag.pic = gag.Picture;
        //                    }
        //                    foreach (Category category in categoriesList)
        //                    {
        //                        categoryList.Add(category);
        //                    }
        //                    var gagObject = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                    var name2 = context.Categories.Where(a => a.CategoryId == gagObject.CategoryId).FirstOrDefault();
        //                    gagObject.Category.CategoryName = name2.CategoryName;
        //                    homeModel.gag = gagObject;
        //                    gagsList.Reverse();
        //                    homeModel.gagList = gagsList;
        //                    homeModel.categoryList = categoryList;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            using (ProjektEntities context = new ProjektEntities())
        //            {
        //                var gagObject = context.Gags.Where(a => a.GagId == id).FirstOrDefault();
        //                var name = context.Categories.Where(a => a.CategoryId == gagObject.CategoryId).FirstOrDefault();
        //                gagObject.Category.CategoryName = name.CategoryName;
        //                homeModel.gag = gagObject;
        //            }
        //        }


        //    return PartialView("GagObject", homeModel);
        //}

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
            List<Albums> categoryList = new List<Albums>();
            List<Pictures> gagsList = new List<Pictures>();
            //List<GagsUser> gagsUsersList = new List<GagsUser>();
            //Gag topGag = new Gag();
            //topGag.Points = 0;
            using (ProjektEntities context = new ProjektEntities())
            {
                var categoriesList = context.Albums.ToList();
                //gagsUsersList = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name).ToList();

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

                //foreach (Category category in categoriesList)
                //{

                //    categoryList.Add(category);
                //}

                //foreach (Pictures gag in context.Pictures)
                //{
                //    if (gag.Points > topGag.Points)
                //        topGag = gag;
                //}
                gagsList.Reverse();
                //homeModel.topGag = topGag;
                homeModel.pictureList = gagsList;
                //homeModel.categoryList = categoryList;
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

        //public ActionResult DeleteProfileGag(int gagId)
        //{
        //    HomeModel homeModel = new HomeModel();
        //    IndexView(homeModel);
        //    ViewBag.Deleted = "true";
        //    using (ProjektEntities context = new ProjektEntities())
        //    {
        //        var gag = context.Gags.Where(a => a.GagId == gagId).FirstOrDefault();
        //        if (gag.Creator != UserId(User.Identity.Name))
        //        {
        //            ViewBag.msg = "Nie możesz usuwać gagów, które nie są Twoje!";
        //            return View("DeleteGag", homeModel);
        //        }
        //        var gagPoints = context.GagsUsers.Where(a => a.GagId == gagId).ToList();
        //        foreach (GagsUser g in gagPoints)
        //        {
        //            context.GagsUsers.Remove(g);
        //        }
        //        context.Gags.Remove(gag);
        //        context.SaveChanges();
        //    }

        //    ViewBag.msg = "Gag został usunięty i nikt już nie będzie w stanie go zobaczyć!";

        //    return View("DeleteGag", homeModel);
        //}
    }
}