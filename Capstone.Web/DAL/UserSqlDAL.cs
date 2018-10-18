using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class UserSqlDAL : IUserDAL
    {
        private string _connectionString;

        public UserSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool CreateUser(User newUser)
        {
            try
            {
                //salt
                string sql = $"INSERT INTO UserInfo VALUES (@username, @email, @password);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", newUser.Username);
                    cmd.Parameters.AddWithValue("@password", newUser.Password);
                    cmd.Parameters.AddWithValue("@avatar", newUser.Email);


                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool ChangePassword(string username, string newPassword)
        {
            try
            {
                string sql = $"UPDATE UserInfo SET password = '{newPassword}' WHERE Username = '{username}'";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


        public List<User> GetUsers()
        {
            List<User> usernames = new List<User>();

            try
            {
                string sql = $"SELECT * FROM UserInfo";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = PopulateUsersFromReader(reader);
                        usernames.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return usernames;
        }

        public List<string> GetUsernames(string startsWith)
        {
            List<string> usernames = new List<string>();

            try
            {
                string sql = $"SELECT Username FROM UserInfo WHERE Username LIKE '{startsWith}%';";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usernames.Add((Convert.ToString(reader["Username"])));
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return usernames;
        }



        private User PopulateUsersFromReader(SqlDataReader reader)
        {
            User item = new User
            {
                Username = Convert.ToString(reader["Username"]),
                Email = Convert.ToString(reader["Email"]),
                Password = Convert.ToString(reader["Password"]),
                RoleId = Convert.ToInt32(reader["RoleId"])
            };
            return item;
        }
    }
}