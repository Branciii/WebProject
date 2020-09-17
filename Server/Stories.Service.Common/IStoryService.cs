using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.Service.Common
{
    public interface IStoryService
    {
        Task<List<StoryModel>> GetStoriesAsync(string UserId);
        Task<StoryModel> GetStoryByIdAsync(Guid StoryID);
        Task PostNewStoryAsync(StoryModel story);
    }
}
