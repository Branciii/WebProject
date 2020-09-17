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
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.SqlClient;

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

        [HttpGet]
        [Route("api/getChapterByNumber")]
        public async Task<HttpResponseMessage> GetChapterByNumberAsync(Guid StoryId, int ChapterNumber)
        { 

            string UserId = RequestContext.Principal.Identity.GetUserId();
            ChapterModel chapterModel = new ChapterModel();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            string queryString =
                "UPDATE USER_STORY SET ChapterNumber = " + ChapterNumber + " WHERE (UserId = '" + UserId + "') AND (StoryId = '" + StoryId + "'); " +
                "SELECT * FROM CHAPTER WHERE StoryId = '" + StoryId + "' AND ChapterNumber = " + ChapterNumber + ";";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                // Call Read before accessing data.
                while (await reader.ReadAsync())
                {
                    chapterModel.ChapterID = reader.GetGuid(0);
                    chapterModel.StoryId = reader.GetGuid(1);
                    chapterModel.Name = reader.GetString(2);
                    chapterModel.ChapterNumber = reader.GetInt32(3);
                    chapterModel.Content = reader.GetString(4);
                }

                // Call Close when done reading.
                reader.Close();

            }
            return Request.CreateResponse(HttpStatusCode.OK, chapterModel);

            //return Request.CreateResponse(HttpStatusCode.OK, await ChapterService.GetChapterByNumberAsync(UserId, StoryId, ChapterNumber));
        }

        [Authorize]
        [HttpGet]
        [Route("api/getChapter")]
        public async Task<HttpResponseMessage> GetChapterAsync(Guid StoryId)
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();
            return Request.CreateResponse(HttpStatusCode.OK, await ChapterService.GetChapterAsync(StoryId,UserId));
        }

        [Authorize]
        [HttpGet]
        [Route("api/isItLastChapter")]
        public async Task<HttpResponseMessage> GetIsItLastChapterAsync(Guid StoryId, int chapterNumber)
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();
            return Request.CreateResponse(HttpStatusCode.OK, await ChapterService.GetIsItLastChapterAsync(StoryId, chapterNumber));
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
