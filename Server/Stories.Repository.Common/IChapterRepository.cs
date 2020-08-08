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
        //Task<List<ChapterModel>> GetChapterAsync(Guid ChapterID);
    }
}
