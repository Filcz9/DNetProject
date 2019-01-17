using System;
using System.Collections.Generic;
using DNetProject.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.Controllers
{
    public class AlbumController : Controller
    {
        // GET: Album
        public ActionResult Album(int albumId)
        {
            HomeModel homeModel = new HomeModel();
            Pictures(homeModel, albumId);
            return View(homeModel);
        }


        public HomeModel Pictures(HomeModel homeModel, int albumId)
        {

            List<Pictures> pictureList = new List<Pictures>();
            using (ProjektEntities context = new ProjektEntities())
            {

                var pictures =
                        from p in context.Pictures
                        from c in context.PicturesAlbums //on p.id equals c.pictures_id into pa
                    where p.id == c.pictures_id && albumId == c.album_id
                        select p;

                foreach (var picture in pictures)
                {
                    pictureList.Add(picture);
                }

                pictureList.Reverse();
                homeModel.pictureList = pictureList;



                return homeModel;
            }
        }
    }
}