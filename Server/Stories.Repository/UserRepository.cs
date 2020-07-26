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
    public class UserRepository : IUserRepository
    {
        public string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebProjectSQL;Integrated Security=True";

        public async Task<bool> PostUserAsync(UserModel userModel)
        {
            string checkIdExistence =
                "SELECT COUNT(*) as count FROM PERSON WHERE Email = '" + userModel.Email + "' OR Username = '" + userModel.Username +"';";
            ;
            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(checkIdExistence, connection);
                await connection.OpenAsync();

                int userCount = (int)await command.ExecuteScalarAsync();
                if (userCount > 0)
                {
                    return false;
                }

                string queryString =
                "INSERT INTO PERSON (PersonID, Username, Password, Email) VALUES ('" + userModel.PersonID + "' ,'" + userModel.Username + "' ,'" + userModel.Password + "' ,'"
                + userModel.Email + "');";

                command =
                    new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                reader.Close();
            }
            return true;
        }
    }
}
