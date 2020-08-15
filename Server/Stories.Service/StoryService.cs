using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Service.Common;
using Stories.Repository.Common;
using Stories.Model;

namespace Stories.Service
{
    public class StoryService : IStoryService
    {
        protected IStoryRepository StoryRepository { get; private set; }

        public StoryService(IStoryRepository storyRepository)
        {
            this.StoryRepository = storyRepository;
        }
        public async Task<List<StoryModel>> GetStoriesAsync(string UserId)
        {
            return await StoryRepository.GetStoriesAsync(UserId);
        }

        public async Task PostNewStoryAsync(StoryModel story)
        {
            await StoryRepository.PostNewStoryAsync(story);
        }

    }
}
