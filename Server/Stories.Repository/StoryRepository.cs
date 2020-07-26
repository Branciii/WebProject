using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Repository.Common;
using Stories.Model;
using System.Data;
using System.Data.SqlClient;

namespace Stories.Repository
{
    public class StoryRepository : IStoryRepository
    {
        public async Task<List<StoryModel>> GetStoriesAsync()
        {
            List<StoryModel> StoryList = new List<StoryModel>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";

            string queryString =
                "SELECT StoryID, AuthorId, Title, Description, Grade, Finished FROM STORY;";

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
                    StoryList.Add(new StoryModel { StoryID = reader.GetGuid(0), AuthorId = reader.GetGuid(1), Title = reader.GetString(2), Description = reader.GetString(3), Grade = reader.GetInt32(4), Finished = reader.GetInt32(5) });
                }

                // Call Close when done reading.
                reader.Close();

            }
            return StoryList;
        }
    }
}
