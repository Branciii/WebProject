using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stories.WebAPI.Models;
using Stories.Service.Common;
using Stories.Model;
using System.Threading.Tasks;
using AutoMapper;

namespace Stories.WebAPI.Controllers
{
    public class ChapterController : ApiController
    {
        protected IChapterService ChapterService { get; private set; }
        protected IMapper Mapper { get; private set; }

        public ChapterController(IChapterService chapterService, IMapper mapper)
        {
            this.ChapterService = chapterService;
            this.Mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [Route("api/getChapter")]
        public async Task<HttpResponseMessage> GetChapterAsync(Guid StoryId, int ChapterNumber)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await ChapterService.GetChapterAsync(StoryId, ChapterNumber));
        }

        [Authorize]
        [HttpPost]
        [Route("api/postNewChapter")]
        public async Task<HttpResponseMessage> PostNewChapterAsync( [FromBody] Chapter chapter)
        {
            ChapterModel chapterModel = Mapper.Map<ChapterModel>(chapter);

            Guid obj = Guid.NewGuid();
            chapterModel.ChapterID = obj;

            await ChapterService.PostNewChapterAsync(chapterModel);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
