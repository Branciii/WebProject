using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.Repository.Common
{
    public interface IChapterRepository
    {
        Task<List<ChapterModel>> GetChaptersAsync(Guid StoryId);
        Task<ChapterModel> GetChapterAsync(Guid StoryId, string UserId);
        Task<ChapterModel> GetChapterByNumberAsync(string UserId, Guid StoryId, int ChapterNumber);
        Task<bool> GetIsItLastChapterAsync(Guid StoryId, int ChapterNumber);
        Task PostNewChapterAsync(ChapterModel chapterModel);
    }
}
