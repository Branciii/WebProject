using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;


namespace Stories.WebAPI.Models
{
    public class Story
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Finished { get; set; }
        public List<Genre> Genres{ get; set; }
    }
}