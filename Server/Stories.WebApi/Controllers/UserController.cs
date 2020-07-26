using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stories.WebApi.Models;
using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Stories.Service.Common;
using System.Threading.Tasks;
using Stories.Model;

namespace Stories.WebApi.Controllers
{
    public class UserController : ApiController
    {
        protected IUserService UserService { get; private set; }
        protected IMapper Mapper { get; private set; }

        public UserController(IUserService userService, IMapper mapper)
        {
            this.UserService = userService;
            this.Mapper = mapper;
        }

        [HttpGet]
        [Route("api/allUsers")]
        public HttpResponseMessage GetUsers()
        {
            List<User> UserList = new List<User>();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";
            string queryString =
                "SELECT Username, Password, Email FROM PERSON;";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    UserList.Add(new User { Username = reader.GetString(0), Password = reader.GetString(1), Email = reader.GetString(2) });
                }


                // Call Close when done reading.
                reader.Close();

                return Request.CreateResponse(HttpStatusCode.OK, UserList);
            }
        }
        [HttpPost]
        [Route("api/newUser")]
        public async Task<HttpResponseMessage> PostUserAsync([FromBody] User user)
        {
            if ((user.Username == "") || (user.Username == null) || (user.Email == "") || (user.Email == null) || (user.Password == "") || (user.Password == null))
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
            }
            UserModel UserModel = Mapper.Map<UserModel>(user);
            Guid obj = Guid.NewGuid();
            UserModel.PersonID = obj;
            if (await UserService.PostUserAsync(UserModel) == false)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}