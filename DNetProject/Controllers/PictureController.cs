using DNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.Controllers
{
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult Picture(int gagId)
        {
            HomeModel homeModel = new HomeModel();
            homeModel.pictureId = gagId;
            PictureView(homeModel, gagId);
            homeModel.comment = new Comments();
            using (ProjektEntities context = new ProjektEntities())
            {
                var checkGag = context.Pictures.Where(a => a.id == gagId).FirstOrDefault();
                if (checkGag == null)
                {
                    return View("NotExist", homeModel);
                }
            }

            return View(homeModel);
        }

        public HomeModel PictureView(HomeModel homeModel, int gagId)
        {
            List<Albums> categoryList = new List<Albums>();
            List<Pictures> gagsList = new List<Pictures>();
            //List<GagsUser> gagsUsersList = new List<GagsUser>();
            List<Comments> pictureComments = new List<Comments>();
            var currentUserId = 0;
            if (User.Identity.Name != "")
            {
                currentUserId = UserId(User.Identity.Name);
            }
            else currentUserId = 0;
            using (ProjektEntities context = new ProjektEntities())
            {
                var privates =
                        from a in context.Albums
                        where a.visibility == 1 && a.id_user == currentUserId
                        select a;
                // gagsUsersList = context.GagsUsers.Where(a => a.User.Username == User.Identity.Name).ToList();
                //var comments = context.Comments.Where(a => a.id_picture == gagId).ToList();
                var comments =
                from c in context.Comments
                join u in context.Users on c.id_user equals u.id into ps
                where c.id_picture == gagId
                select new { Comment = c, Users = ps};

                var sGag = context.Pictures.Where(a => a.id == gagId).FirstOrDefault();

                foreach (var comment in comments)
                {
                    pictureComments.Add(comment.Comment);
                }
                foreach (var category in privates)
                {
                    categoryList.Add(category);
                }

                gagsList.Add(sGag);
                gagsList.Reverse();
                homeModel.pictureComments = pictureComments;
                homeModel.pictureList = gagsList;
                homeModel.privateList = categoryList;
                //homeModel.gagsUserList = gagsUsersList;

                return homeModel;
            }
        }

        public ActionResult Comment(Comments Comment ,int id)
        {
            HomeModel homeModel = new HomeModel();
            List<Comments> pictureComments = new List<Comments>();
            var currentUserId = UserId(User.Identity.Name);
            using (ProjektEntities context = new ProjektEntities())
            {

                Comments Comment2 = new Comments
                {
                    text = Comment.text,
                    id_user = currentUserId,
                    id_picture = id
                };
                context.Comments.Add(Comment2);
                context.SaveChanges();

                var comments =
                from c in context.Comments
                join u in context.Users on c.id_user equals u.id into ps
                where c.id_picture == id
                select new { Comment = c, Users = ps };

                foreach (var comment in comments)
                {
                    pictureComments.Add(comment.Comment);
                }
                homeModel.pictureComments = pictureComments;
            }
 
            return PartialView("Comment", homeModel);
        }

        public ActionResult AddToPrivate(Albums album, int id)
        {
            HomeModel homeModel = new HomeModel();
            List<Comments> pictureComments = new List<Comments>();
            var currentUserId = UserId(User.Identity.Name);
            using (ProjektEntities context = new ProjektEntities())
            {

                PicturesAlbums picturesAlbums = new PicturesAlbums
                {
                    pictures_id = id,
                    album_id = album.id
                };

                context.PicturesAlbums.Add(picturesAlbums);
                context.SaveChanges();

                var comments =
                from c in context.Comments
                join u in context.Users on c.id_user equals u.id into ps
                where c.id_picture == id
                select new { Comment = c, Users = ps };

                foreach (var comment in comments)
                {
                    pictureComments.Add(comment.Comment);
                }
                homeModel.pictureComments = pictureComments;
            }

            return PartialView("Comment", homeModel);
        }
        //public ActionResult Upvote(int id, string categoryName)
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
        //    GagView(homeModel, id);


        //    return PartialView("GagPoints", homeModel);
        //}

        //public ActionResult Downvote(int id, string categoryName)
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
        //    GagView(homeModel, id);



        //    return PartialView("GagPoints", homeModel);
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
    }
}