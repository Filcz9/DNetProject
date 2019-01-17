using System;
using System.Collections.Generic;
using DNetProject.Models.Extended;
using System.Linq;
using System.Web;

namespace DNetProject.Models
{
    public class HomeModel
    {
        public Pictures picture { get; set; }
        public Albums album { get; set; }
        public Comments comment { get; set; }
        public int pictureId { get; set; }
        public string text { get; set; }
      //  public List<> test { get; set; }
        //public GagsUser gagsUsers { get; set; }
       // public Gag topGag { get; set; }
        //public IEnumerable<Gag> gagList { get; set; }
        //public IEnumerable<Category> categoryList { get; set; }
        public UserLogin login { get; set; }
        public List<Comments> pictureComments { get; set; }
        public List<Pictures> pictureList { get; set; }
        public List<Albums> albumList { get; set; }
        public List<Albums> privateList { get; set; }
        //public List<GagsUser> gagsUserList { get; set; }
    }
}