using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.Service.Common
{
    public interface IChapterService
    {
        Task<List<ChapterModel>> GetChaptersAsync(Guid StoryId);
        Task<ChapterModel> GetChapterAsync(Guid StoryId, int ChapterNumber);
        Task PostNewChapterAsync(ChapterModel chapterModel);
        //Task<List<ChapterModel>> GetChapterAsync(Guid ChapterID);
    }
}
