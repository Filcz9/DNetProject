using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNetProject.CustomHelpers
{
    public static class CustomHTMLHelpers
    {
        public static IHtmlString Image(this HtmlHelper helper, string src)
        {
            TagBuilder tb = new TagBuilder("img");
            string src2 = "~/UserPictureImages/" + src;
            string style = "width: 550px";
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src2));
            tb.Attributes.Add("style", style);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
        public static IHtmlString Comment(this HtmlHelper helper, string username, string text)
        {
            string comment = $" <div class=\"commentText\"><span class=\"date sub-text\">{username}</span><p>{text}</p></div>";
            
            return new HtmlString(comment);
        }

    }
}