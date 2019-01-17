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
                name: "ModeratorApplicationRoute",
                url: "ModeratorApplication",
                defaults: new { controller = "ModeratorApplication", action = "ModeratorApplication" }
            );

            routes.MapRoute(
             name: "PictureRoute",
             url: "Picture/{gagId}",
             defaults: new { controller = "Picture", action = "Picture" }
         );

            routes.MapRoute(
              name: "ProfileRoute",
              url: "Profile",
              defaults: new { controller = "Profile", action = "ProfileIndex" }
          );

            routes.MapRoute(
              name: "UserListRoute",
              url: "AdminPanel/ListaUżytkowników",
              defaults: new { controller = "AdminPanel", action = "UserList" }
          );

            routes.MapRoute(
               name: "AdminPanelRoute",
               url: "AdminPanel",
               defaults: new { controller = "AdminPanel", action = "AdminPanel" }
           );

            routes.MapRoute(
               name: "UploadRoute",
               url: "Upload",
               defaults: new { controller = "Upload", action = "Upload" }
           );

            routes.MapRoute(
               name: "CategoryRoute",
               url: "{categoryName}",
               defaults: new { controller = "Home", action = "IndexCategory"}
           );
            routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);

        }
    }
}
