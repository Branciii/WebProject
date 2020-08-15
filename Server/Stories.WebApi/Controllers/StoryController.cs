﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Stories.WebAPI.Models;
using AutoMapper;
using Stories.Service.Common;
using Stories.Model;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
using System.Data;
using Stories.Repository;

namespace Stories.WebAPI.Controllers
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
        [Authorize]
        [Route("api/getStories")]
        public async Task<HttpResponseMessage> GetStoriesAsync()
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();

            List<StoryModel> StoryList = await StoryService.GetStoriesAsync(UserId);
            if (StoryList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            //List<Story> Stories = Mapper.Map<List<Story>>(StoryList);

            return Request.CreateResponse(HttpStatusCode.OK, StoryList);
        }

        [HttpPost]
        [Authorize]
        [Route("api/postNewStory")]
        public async Task<HttpResponseMessage> PostNewStoryAsync(Story story)
        {
            StoryModel storyModel = Mapper.Map<StoryModel>(story);

            string UserId = RequestContext.Principal.Identity.GetUserId();

            storyModel.AuthorId = UserId;

            Guid obj = Guid.NewGuid();
            storyModel.StoryID = obj;

            await StoryService.PostNewStoryAsync(storyModel);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
