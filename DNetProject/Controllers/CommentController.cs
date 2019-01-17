using DNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Post(Comments Comment, Pictures Picture)
        {


            var currentUserId = UserId(User.Identity.Name);
            using (ProjektEntities context = new ProjektEntities())
                {

                    Comments Comment2 = new Comments
                    {
                        text = Comment.text,
                        id_user = currentUserId,
                        id_picture = Picture.id                         
                    };
                    context.Comments.Add(Comment2);
                    context.SaveChanges();
             }
              return PartialView("Comment");

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