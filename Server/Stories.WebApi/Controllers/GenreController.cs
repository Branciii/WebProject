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
        }
    }
}
