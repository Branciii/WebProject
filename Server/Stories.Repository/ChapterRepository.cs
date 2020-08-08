using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stories.Repository.Common;
using Stories.Model;
using System.Data.SqlClient;

namespace Stories.Repository
{
    public class ChapterRepository : IChapterRepository
    {
        public async Task<List<ChapterModel>> GetChaptersAsync(Guid StoryId)
        {
            List<ChapterModel> ChapterList = new List<ChapterModel>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            string queryString =
                "SELECT * FROM CHAPTER WHERE StoryId = '" + StoryId + "';";

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
                    ChapterList.Add(new ChapterModel { ChapterID = reader.GetGuid(0), StoryId = reader.GetGuid(1),
                    Name = reader.GetString(2), ChapterNumber = reader.GetInt32(3), Content = reader.GetString(4)});
                }

                // Call Close when done reading.
                reader.Close();

            }
            return ChapterList;
        }
    }
}
