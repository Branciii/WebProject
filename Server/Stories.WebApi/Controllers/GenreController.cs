using System;
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

namespace Stories.WebAPI.Controllers
{
    public class GenreController : ApiController
    {
        protected IGenreService GenreService { get; private set; }
        protected IMapper Mapper { get; private set; }

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            this.GenreService = genreService;
            this.Mapper = mapper;
        }


        /*
        [HttpGet]
        [Authorize]
        [Route("api/getGenres")]
        public async Task<HttpResponseMessage> GetGenresAsync()
        {
            List<GenreModel> GenreList = await GenreService.GetGenresAsync();
            if (GenreList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            List<Genre> Genres = Mapper.Map<List<Genre>>(GenreList);

            return Request.CreateResponse(HttpStatusCode.OK, Genres);
        }*/

        [HttpGet]
        [Authorize]
        [Route("api/getGenres")]
        public async Task<HttpResponseMessage> GetGenresAsync()
        {
            List<GenreModel> GenreList = await GenreService.GetGenresAsync();
            if (GenreList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, GenreList);
        }


        [HttpGet]
        [Authorize]
        [Route("api/getUsersGenres")]
        public async Task<HttpResponseMessage> GetUsersGenresAsync()
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();

            List<GenreModel> GenreList = await GenreService.GetUsersGenresAsync(UserId);
            if (GenreList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, GenreList);
        }

        [HttpGet]
        [Authorize]
        [Route("api/getOtherGenres")]
        public async Task<HttpResponseMessage> GetOtherGenresAsync()
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();

            List<GenreModel> GenreList = await GenreService.GetUsersGenresAsync(UserId);
            if (GenreList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, GenreList);
        }

        [HttpGet]
        [Authorize]
        [Route("api/getStoryGenres")]
        public async Task<HttpResponseMessage> GetStoryGenresAsync(Guid StoryId)
        {
            List<GenreModel> GenreList = await GenreService.GetStoryGenresAsync(StoryId);
            if (GenreList.Count() == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, GenreList);
        }

        [HttpPost]
        [Authorize]
        [Route("api/postUserGenres")]
        public async Task<HttpResponseMessage> PostUserGenresAsync(List<GenreModel> genreModels)
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();

            await GenreService.PostUserGenresAsync(UserId, genreModels);
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
