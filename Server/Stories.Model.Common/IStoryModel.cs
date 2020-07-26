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
        Guid AuthorId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        int Grade { get; set; }
        int Finished { get; set; }
    }
}
