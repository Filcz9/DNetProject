using DNetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        // GET: Upload
        //[Authorize]
        public ActionResult Upload()
        {

            Picture picture = new Picture();

            using (ProjektEntities context = new ProjektEntities())
            {
                picture.AlbumCollection = context.Albums.ToList<Album>();
            }
            return View(picture);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, Picture Picture, Album album)
        {


            var currentUserId = UserId(User.Identity.Name);
            if (file != null && Picture.title != null && Picture.Albums != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/UserPictureImages/"), pic);
                string pathDB = pic;
                // file is uploaded
                file.SaveAs(path);
                
                using (ProjektEntities context = new ProjektEntities())
                {
                    
                    Picture Picture2 = new Picture
                    {
                        title = Picture.title,
                        img = pathDB,                        
                        id_user = currentUserId
                    };
                    context.Pictures.Add(Picture2);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Picture PictureModel = new Picture();

                using (ProjektEntities context = new ProjektEntities())
                {
                    PictureModel.AlbumCollection = context.Albums.ToList<Album>();
                }
                return View("Upload", PictureModel);
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
