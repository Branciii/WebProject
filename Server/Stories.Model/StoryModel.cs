using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model.Common;

namespace Stories.Model
{
    public class StoryModel : IStoryModel
    {
        public Guid StoryID { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }
        public int Finished { get; set; }
        public List<GenreModel> Genres { get; set; }
    }
}
