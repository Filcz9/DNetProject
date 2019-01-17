using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DNetProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


             routes.MapRoute(
                name: "EditUserRoute",
                url: "EditUser",
                defaults: new { controller = "CRUD", action = "EditUser" }
            );

            routes.MapRoute(
             name: "PictureRoute",
             url: "Picture/{picId}",
             defaults: new { controller = "Picture", action = "Picture" }
         );

            routes.MapRoute(
              name: "VerifyAgeRoute",
              url: "VerifyAge",
              defaults: new { controller = "Verify", action = "VerifyAge" }
          );

            routes.MapRoute(
              name: "UserListRoute",
              url: "CRUD/UserList",
              defaults: new { controller = "CRUD", action = "UserList" }
          );

            routes.MapRoute(
               name: "AdminPanelRoute",
               url: "Crud",
               defaults: new { controller = "CRUD", action = "AdminPanel" }
           );

            routes.MapRoute(
               name: "UploadRoute",
               url: "Upload",
               defaults: new { controller = "Upload", action = "Upload" }
           );

            routes.MapRoute(
               name: "AlbumRoute",
               url: "Album/{albumId}",
               defaults: new { controller = "Album", action = "Album"}
           );
            routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);

        }
    }
}
