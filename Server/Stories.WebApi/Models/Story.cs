using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Stories.Model;


namespace Stories.WebAPI.Models
{
    public class Story
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<GenreModel> Genres { get; set; }
    }
}