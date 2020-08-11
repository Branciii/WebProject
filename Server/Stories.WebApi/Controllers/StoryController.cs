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
using System.Data.SqlClient;
using System.Data;

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


        /*
        [HttpGet]
        [Authorize]
        [Route("api/getStories")]
        public async Task<HttpResponseMessage> GetStoriesAsync()
        {
            string UserId = RequestContext.Principal.Identity.GetUserId();
            string queryString = "";
            string debug = "";
            List<StoryModel> StoryList = new List<StoryModel>();
            

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            // provjera ima li korisnik najdrazih zanrova
            string findFavGenres =
                "SELECT COUNT(*) as count FROM USER_GENRE WHERE UserId = '" + UserId + "';";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(findFavGenres, connection);
                await connection.OpenAsync();

                int count = (int)await command.ExecuteScalarAsync();
                //korisnik ima najdraze zanrove pa će mu se prvo pojaviti priče u tom žanru
                if (count > 0)
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName, g.GenreID, g.Name " +
                                "FROM STORY s " +
                                "JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                "JOIN AspNetUsers anu ON(anu.id = s.AuthorId)" +
                                "JOIN USER_GENRE ug ON(sg.GenreId = ug.GenreId) AND(ug.UserId = '" + UserId + "')" +
                                "JOIN GENRE g ON (sg.GenreId=g.GenreID)" +
                                "ORDER BY s.Grade";
                }
                else
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName, g.GenreID, g.Name " +
                                "FROM STORY s " +
                                "JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                "JOIN AspNetUsers anu ON(anu.id = s.AuthorId) " +
                                "JOIN GENRE g ON (sg.GenreId=g.GenreID)" +
                                "ORDER BY s.Grade; ";
                }

                command =
                    new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                Guid lastStId = Guid.NewGuid();
                List<GenreModel> GenreList = new List<GenreModel>();
                while (await reader.ReadAsync())
                {
                    debug += "                                                 ";
                    debug += "                                                 ";
                    debug += "                                                              ";
                    debug += "TRY " + "lastStId: " + lastStId + " StoryId: " + reader.GetGuid(0) +
                        " Title: " + reader.GetString(2) +" Genre name: " + reader.GetString(8);
                    Guid StoryId = reader.GetGuid(0);
    
                    if (lastStId == StoryId)
                    {
                        GenreList.Add(new GenreModel { GenreID = reader.GetGuid(7), Name = reader.GetString(8) });

                    }
                    else
                    {
                        GenreList = new List<GenreModel>();
                        GenreList.Add(new GenreModel { GenreID = reader.GetGuid(7), Name = reader.GetString(8) });
                        StoryList.Add(new StoryModel
                        {
                            StoryID = StoryId,
                            AuthorId = reader.GetString(1),
                            Title = reader.GetString(2),
                            Description = reader.GetString(3),
                            Grade = reader.GetInt32(4),
                            Finished = reader.GetInt32(5),
                            Author = reader.GetString(6),
                            Genres = GenreList
                        });
                    }
                    foreach (var story in StoryList)
                    {
                        debug += " current genres for story : " + story.Title + " are : ";
                        foreach (var genre in story.Genres)
                        {
                            debug += genre.Name;
                        }
                    }
                    
                    lastStId = StoryId;
                }

                // Call Close when done reading.
                reader.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, StoryList);
        }*/
    }
}
