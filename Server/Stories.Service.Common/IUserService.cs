using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.Service.Common
{
    public interface IUserService
    {
        //Task<List<StoryModel>> GetStoriesAsync();

        Task<bool> PostUserAsync(UserModel userModel);

        //Task<bool> UpdateStoryAsync(StoryModel storyModel);

        //Task<bool> DeleteStoryAsync(Guid storyID);
    }
}
