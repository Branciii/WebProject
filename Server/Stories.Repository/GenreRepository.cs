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
    public class GenreRepository : IGenreRepository
    {
        public async Task<List<GenreModel>> GetGenresAsync()
        {
            List<GenreModel> GenreList = new List<GenreModel>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";

            string queryString =
                "SELECT GenreID, Name FROM GENRE;";

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
                    GenreList.Add(new GenreModel { GenreID = reader.GetGuid(0), Name = reader.GetString(1) });
                }

                // Call Close when done reading.
                reader.Close();

            }
            return GenreList;
        }

        public async Task<List<GenreModel>> GetUsersGenresAsync(string UserId)
        {
            List<GenreModel> GenreList = new List<GenreModel>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";

            string queryString =
                "SELECT g.GenreID,g.Name FROM USER_GENRE u JOIN GENRE g ON (g.GenreID = u.GenreId) WHERE UserId = '" + UserId + "';";

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
                    GenreList.Add(new GenreModel { GenreID = reader.GetGuid(0), Name = reader.GetString(1) });
                }

                // Call Close when done reading.
                reader.Close();

            }
            return GenreList;
        }

        public async Task<bool> PostUsersGenreAsync(string UserId, string GenreName)
        {
            Guid GenreId = default;

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";

            string findGenreId =
                "SELECT GenreID FROM GENRE WHERE Name = '" + GenreName + "';";

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(findGenreId, connection);
                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                // Call Read before accessing data.
                while (await reader.ReadAsync())
                {
                    GenreId = reader.GetGuid(0);
                }

                string checkExistence =
                "SELECT COUNT(*) as count FROM USER_GENRE WHERE UserId = '" + UserId + "' AND" +
                "GenreId = '" + GenreId + "';";

                command =
                    new SqlCommand(checkExistence, connection);
                await connection.OpenAsync();

                int count = (int)await command.ExecuteScalarAsync();
                if (count > 0)
                {
                    return false;
                }

                string queryString =
                "INSERT INTO USER_GENRE (UserId, GenreId) VALUES ('" + UserId + "', '" + GenreId +"');";

                command =
                    new SqlCommand(queryString, connection);
                await connection.OpenAsync();

                reader = await command.ExecuteReaderAsync();

                // Call Close when done reading.
                reader.Close();
            }
            return true;
        }
    }
}
