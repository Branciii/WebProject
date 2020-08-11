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
    public class ChapterService : IChapterService
    {
        protected IChapterRepository ChapterRepository { get; private set; }

        public ChapterService(IChapterRepository chapterRepository)
        {
            this.ChapterRepository = chapterRepository;
        }

        public async Task<List<ChapterModel>> GetChaptersAsync(Guid StoryId)
        {
            return await ChapterRepository.GetChaptersAsync(StoryId);
        }

        public async Task<ChapterModel> GetChapterAsync(Guid StoryId, int ChapterNumber)
        {
            return await ChapterRepository.GetChapterAsync(StoryId, ChapterNumber);
        }

        public async Task PostNewChapterAsync(ChapterModel chapterModel)
        {
            await ChapterRepository.PostNewChapterAsync(chapterModel);
        }


    }
}
