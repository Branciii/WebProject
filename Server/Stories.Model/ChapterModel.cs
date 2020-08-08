using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Model
{
    public class ChapterModel
    {
        public Guid ChapterID { get; set; }
        public Guid StoryId { get; set; }
        public string Name { get; set; }
        public int ChapterNumber { get; set; }
        public string Content { get; set; }
    }
}
