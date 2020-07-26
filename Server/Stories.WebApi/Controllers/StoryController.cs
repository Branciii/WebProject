using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Stories.Service.Common;
using System.Threading.Tasks;
using Stories.Model;
using Stories.WebApi.Models;

namespace Stories.WebApi.Controllers
{
    public class StoryController : ApiController
    {
        protected IStoryService StoryService { get; private set; }
        protected IMapper Mapper { get; private set; }

        public StoryController(IStoryService storyService, IMapper mapper)
        {
            this.StoryService = storyService;
            this.Mapper = mapper;
        }

        [HttpGet]
        [Route("api/getStories")]
        public async Task<HttpResponseMessage> GetStoriesAsync()
        {
            List<StoryModel> StoryList = await StoryService.GetStoriesAsync();
            if (StoryList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            List<Story> Stories = Mapper.Map<List<Story>>(StoryList);
            
            return Request.CreateResponse(HttpStatusCode.OK, Stories);
        }
    }
}
