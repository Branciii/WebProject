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
using System.ComponentModel.DataAnnotations;

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
            List<Story> Stories = Mapper.Map<List<Story>>(StoryList);

            return Request.CreateResponse(HttpStatusCode.OK, Stories);
        }

        [HttpPost]
        [Route("api/parexample")]
        public HttpResponseMessage ParEx([FromBody] string content)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand go = new SqlCommand();

                con.Open();
                go.Connection = con;
                go.CommandText = "INSERT INTO PAREXAMPLE (Content) VALUES (@InsuredID);";
                go.Parameters.Add("@InsuredID", SqlDbType.NVarChar, -1).Value = content;

                SqlDataReader readIn = go.ExecuteReader();
                while (readIn.Read())
                {
                    // reading data from reader
                }

                con.Close();

                // other stuff
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
