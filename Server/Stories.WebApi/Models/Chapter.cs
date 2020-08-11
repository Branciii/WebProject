using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stories.WebAPI.Models
{
    public class Chapter
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid StoryId { get; set; }
        public int ChapterNumber { get; set; }
    }
}