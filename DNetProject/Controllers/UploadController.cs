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

            Pictures picture = new Pictures();

            using (ProjektEntities context = new ProjektEntities())
            {
                picture.AlbumCollection = context.Albums.Where(a => a.visibility == 0).ToList<Albums>();
                   // context.Albums.ToList<Albums>();
            }
            return View(picture);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, Pictures Picture)
        {

            
            var currentUserId = UserId(User.Identity.Name);
            if (file != null && Picture.title != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/UserPictureImages/"), pic);
                string pathDB = pic;
                // file is uploaded
                file.SaveAs(path);
                
                using (ProjektEntities context = new ProjektEntities())
                {

                    Pictures Picture2 = new Pictures
                    {
                        AlbumId = Picture.AlbumId,
                        title = Picture.title,
                        description = Picture.description,
                        img = pic,
                        id_user = currentUserId,
                        
                    };
                    context.Pictures.Add(Picture2);
                    PicturesAlbums pical = new PicturesAlbums
                    {
                        album_id = Picture2.AlbumId,
                        pictures_id = Picture2.id
                    };
                    context.PicturesAlbums.Add(pical);
                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Pictures PictureModel = new Pictures();

                using (ProjektEntities context = new ProjektEntities())
                {
                    PictureModel.AlbumCollection = context.Albums.ToList<Albums>();
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
