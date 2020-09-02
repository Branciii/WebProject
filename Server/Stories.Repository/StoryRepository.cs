﻿using System;
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
        public async Task<List<StoryModel>> GetStoriesAsync(string UserId)
        {
            string queryString = "";
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

                //korisnik ima najdraze zanrove
                if (count > 0)
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName, g.GenreID, g.Name " +
                                "FROM STORY s" +
                                " JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                " JOIN AspNetUsers anu ON(anu.id = s.AuthorId) JOIN USER_GENRE ug ON(sg.GenreId = ug.GenreId) AND(ug.UserId = '" + UserId + "')" +
                                " JOIN STORY_GENRE sgg ON(s.StoryID = sgg.StoryId)" +
                                " JOIN GENRE g ON(sgg.GenreId= g.GenreID)" +
                                " ORDER BY s.Grade;";
                }
                else
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName, g.GenreID, g.Name " +
                                "FROM STORY s " +
                                "JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                "JOIN AspNetUsers anu ON(anu.id = s.AuthorId) " +
                                "JOIN STORY_GENRE sgg ON (s.StoryID = sgg.StoryId) " +
                                "JOIN GENRE g ON (sgg.GenreId=g.GenreID)" +
                                "ORDER BY s.Grade; ";
                }

                command =
                    new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                Guid lastStId = Guid.NewGuid();
                List<GenreModel> GenreList = new List<GenreModel>();
                while (await reader.ReadAsync())
                {
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

                    lastStId = StoryId;
                }

                // Call Close when done reading.
                reader.Close();
            }
            return StoryList;
        }

        public async Task PostNewStoryAsync(StoryModel story)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProject;Integrated Security=True";
            string queryString =
                "INSERT INTO STORY (StoryID, AuthorId, Title, Description) VALUES ('" + story.StoryID + "' ,'" + story.AuthorId + "' ,@Title ,@Description);";

            foreach (GenreModel genre in story.Genres)
            {
                queryString += "INSERT INTO STORY_GENRE (StoryId, GenreId) VALUES ( '" + story.StoryID + "', '" + genre.GenreID + "');" ;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand go = new SqlCommand();

                await con.OpenAsync();
                go.Connection = con;
                go.CommandText = queryString;
                go.Parameters.Add("@Title", SqlDbType.VarChar, -1).Value = story.Title;
                go.Parameters.Add("@Description", SqlDbType.VarChar, -1).Value = story.Description;

                SqlDataReader readIn = await go.ExecuteReaderAsync();

                con.Close();

            }
        }
    }
}

/*using System;
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
        public async Task<List<StoryModel>> GetStoriesAsync(string UserId)
        {
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

                string queryString = "";
                int count = (int)await command.ExecuteScalarAsync();
                //korisnik ima najdraze zanrove pa će mu se prvo pojaviti priče u tom žanru
                if (count > 0)
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName " +
                                "FROM STORY s " +
                                "JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                "JOIN AspNetUsers anu ON(anu.id = s.AuthorId)" +
                                "JOIN USER_GENRE ug ON(sg.GenreId = ug.GenreId) AND(ug.UserId = '" + UserId + "')" +
                                "ORDER BY s.Grade";
                }
                else
                {
                    queryString = "SELECT DISTINCT s.StoryID, s.AuthorId, s.Title, s.Description, s.Grade, s.Finished, anu.UserName " +
                                "FROM STORY s " +
                                "JOIN STORY_GENRE sg ON(s.StoryID = sg.StoryId) AND(s.AuthorId != '" + UserId + "')" +
                                "JOIN AspNetUsers anu ON(anu.id = s.AuthorId) " +
                                "ORDER BY s.Grade; ";
                }

                command =
                    new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    StoryList.Add(new StoryModel
                    {
                        StoryID = reader.GetGuid(0),
                        AuthorId = reader.GetString(1),
                        Title = reader.GetString(2),
                        Description = reader.GetString(3),
                        Grade = reader.GetInt32(4),
                        Finished = reader.GetInt32(5),
                        Author = reader.GetString(6)
                    });
                }

                // Call Close when done reading.
                reader.Close();
            }
            // brisanje duplikata
            StoryList = StoryList.GroupBy(x => x.StoryID).Select(y => y.First()).ToList();
            return StoryList;
        }
    }
}
*/
