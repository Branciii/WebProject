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

        public async Task<ChapterModel> GetChapterAsync(Guid StoryId, string UserId)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";
            ChapterModel chapterModel = new ChapterModel();

            string checkExistence =
            "SELECT COUNT(*) as count FROM USER_STORY WHERE UserId = '" + UserId + "' AND StoryId = '" + StoryId + "';";
            

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                string queryString = "";

                SqlCommand command =
                    new SqlCommand(checkExistence, connection);
                await connection.OpenAsync();

                int Count = (int)await command.ExecuteScalarAsync();
                if (Count == 0)
                {
                    queryString = 
                        "INSERT INTO USER_STORY (UserId, StoryId, ChapterNumber) VALUES ('" + UserId +"','" + StoryId + "'," + 1 + "); "+
                        "SELECT ChapterID, StoryId, Name, ChapterNumber, Content FROM CHAPTER WHERE (ChapterNumber = 1)" +
                        "AND (StoryId = '" + StoryId + "');";
                }
                else
                {
                    queryString = "SELECT c.ChapterID, c.StoryId, c.Name, c.ChapterNumber, c.Content " +
                                    "FROM CHAPTER c JOIN USER_STORY us " +
                                    "ON (c.StoryId = us.StoryId) " +
                                    "AND (c.StoryId = '" + StoryId + "')" +
                                    "AND(us.UserId = '" + UserId + "') " +
                                    "AND(us.ChapterNumber = c.ChapterNumber); ";
                }

                command =
                    new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    chapterModel.ChapterID = reader.GetGuid(0);
                    chapterModel.StoryId = reader.GetGuid(1);
                    chapterModel.Name = reader.GetString(2);
                    chapterModel.ChapterNumber = reader.GetInt32(3);
                    chapterModel.Content = reader.GetString(4);
                }

                reader.Close();
            }
            return chapterModel;
        }

        public async Task<ChapterModel> GetChapterByNumberAsync(string UserId, Guid StoryId, int ChapterNumber)
        {
            ChapterModel chapterModel = new ChapterModel();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            string queryString =
                "UPDATE USER_STORY SET ChapterNumber = " + ChapterNumber + " WHERE (UserId = '" + UserId + "') AND (StoryId = '" + StoryId +  "'); " +
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
            return chapterModel;
        }

        public async Task<bool> GetIsItLastChapterAsync(Guid StoryId, int ChapterNumber)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            string queryString = "SELECT MAX(c.ChapterNumber) FROM CHAPTER c WHERE StoryId = '" + StoryId + "' AND (c.Content IS NOT NULL);";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                await connection.OpenAsync();

                int Max = (int)await command.ExecuteScalarAsync();

                if (Max == ChapterNumber)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task PostNewChapterAsync(ChapterModel chapterModel)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand go = new SqlCommand();

                await con.OpenAsync();
                go.Connection = con;
                go.CommandText = "INSERT INTO CHAPTER (ChapterID, StoryId, Name, ChapterNumber, Content) VALUES ('"+ 
                   chapterModel.ChapterID + "', '" + chapterModel.StoryId + "', '" + chapterModel.Name + "', " + chapterModel.ChapterNumber
                   + ", @Content);";
                go.Parameters.Add("@Content", SqlDbType.NVarChar, -1).Value = chapterModel.Content;

                SqlDataReader readIn = await go.ExecuteReaderAsync();

                con.Close();

            }
        }
    }
}
