using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Model.Common
{
    public interface IStoryModel
    {
        Guid StoryID { get; set; }
        string AuthorId { get; set; }
        string Author { get; set; }
        string Title { get; set; }
        int Grade { get; set; }
        string Description { get; set; }
        int Finished { get; set; }
    }
}
