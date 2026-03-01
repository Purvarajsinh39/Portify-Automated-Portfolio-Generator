using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Portify.Models
{
    public class PortifyDbContext
    {
        private string _connectionString;

        public PortifyDbContext()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PortifyDB"].ConnectionString;
        }

        public User GetUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }
                return null;
            }
        }

        public void AddUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (FullName, Email, PasswordHash, Role, IsBlocked, CreatedAt) VALUES (@FullName, @Email, @PasswordHash, @Role, @IsBlocked, @CreatedAt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@IsBlocked", user.IsBlocked);
                cmd.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapUser(reader));
                    }
                }
            }
            return users;
        }

        public void UpdateUserStatus(int userId, bool isBlocked)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET IsBlocked = @IsBlocked WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IsBlocked", isBlocked);
                cmd.Parameters.AddWithValue("@Id", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapUser(reader);
                    }
                }
                return null;
            }
        }

        public void UpdateUserProfile(int userId, string fullName, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET FullName = @FullName, PasswordHash = @PasswordHash WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@Id", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"].ToString(),
                PasswordHash = reader["PasswordHash"].ToString(),
                Role = reader["Role"].ToString(),
                IsBlocked = (bool)reader["IsBlocked"],
                CreatedAt = (DateTime)reader["CreatedAt"]
            };
        }
    }
}
