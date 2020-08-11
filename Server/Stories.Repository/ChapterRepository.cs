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

        public async Task<ChapterModel> GetChapterAsync(Guid StoryId, int ChapterNumber)
        {
            ChapterModel chapterModel = new ChapterModel();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";

            string queryString =
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

        public async Task PostNewChapterAsync(ChapterModel chapterModel)
        {
            //mozda dodati da prvo provjeri postoji li priča, ali valjda neće moći ni doći do tog na frontendu da pošalje ovaj zahtjev
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
