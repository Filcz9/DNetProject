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
        public ActionResult Picture(int picId)
        {
            HomeModel homeModel = new HomeModel();
            homeModel.pictureId = picId;
            PictureView(homeModel, picId);
            homeModel.comment = new Comments();
            using (ProjektEntities context = new ProjektEntities())
            {
                var checkGag = context.Pictures.Where(a => a.id == picId).FirstOrDefault();
                if (checkGag == null)
                {
                    return View("NotExist", homeModel);
                }
            }

            return View(homeModel);
        }

        public HomeModel PictureView(HomeModel homeModel, int gagId)
        {
            List<Albums> albumList = new List<Albums>();
            List<Pictures> pictureList = new List<Pictures>();
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
                    albumList.Add(category);
                }

                pictureList.Add(sGag);
                pictureList.Reverse();
                homeModel.pictureComments = pictureComments;
                homeModel.pictureList = pictureList;
                homeModel.privateList = albumList;


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