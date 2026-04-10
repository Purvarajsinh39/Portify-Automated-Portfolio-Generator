using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using Portify.Models; // Explicitly add this again

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
                string query = "INSERT INTO Users (FullName, Email, PasswordHash, Role, IsBlocked, BlockReason, ReceiveNotifications, CreatedAt) VALUES (@FullName, @Email, @PasswordHash, @Role, @IsBlocked, @BlockReason, @ReceiveNotifications, @CreatedAt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.Parameters.AddWithValue("@IsBlocked", user.IsBlocked);
                cmd.Parameters.AddWithValue("@BlockReason", user.BlockReason ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ReceiveNotifications", user.ReceiveNotifications);
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

        public void UpdateUserStatus(int userId, bool isBlocked, string reason = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET IsBlocked = @IsBlocked, BlockReason = @BlockReason WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IsBlocked", isBlocked);
                cmd.Parameters.AddWithValue("@BlockReason", reason ?? (object)DBNull.Value);
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

        public void UpdateUserProfile(int userId, string fullName, string passwordHash, bool receiveNotifications)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Users SET FullName = @FullName, PasswordHash = @PasswordHash, ReceiveNotifications = @ReceiveNotifications WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                cmd.Parameters.AddWithValue("@ReceiveNotifications", receiveNotifications);
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
                BlockReason = reader["BlockReason"] != DBNull.Value ? reader["BlockReason"].ToString() : null,
                ReceiveNotifications = reader["ReceiveNotifications"] != DBNull.Value ? (bool)reader["ReceiveNotifications"] : true,
                CreatedAt = (DateTime)reader["CreatedAt"]
            };
        }

        // --- Template Methods ---
        
        public List<Template> GetAllTemplates()
        {
            List<Template> templates = new List<Template>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Templates";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        templates.Add(MapTemplate(reader));
                    }
                }
            }
            return templates;
        }

        public Template GetTemplateById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM Templates WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapTemplate(reader);
                    }
                }
                return null;
            }
        }

        public void AddTemplate(Template template)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Templates (TemplateName, Description, ThumbnailPath, HtmlPath, CssPath, ConfigPath, IsActive) VALUES (@Name, @Description, @ThumbnailPath, @FilePath, @CssPath, @ConfigPath, @IsActive)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", template.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", template.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ThumbnailPath", template.ThumbnailPath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FilePath", template.FilePath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CssPath", template.CssPath ?? "");
                cmd.Parameters.AddWithValue("@ConfigPath", template.ConfigPath ?? "");
                cmd.Parameters.AddWithValue("@IsActive", template.IsActive);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateTemplate(Template template)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Templates SET TemplateName = @Name, Description = @Description, ThumbnailPath = @ThumbnailPath, HtmlPath = @FilePath, CssPath = @CssPath, ConfigPath = @ConfigPath, IsActive = @IsActive WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", template.Name ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Description", template.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ThumbnailPath", template.ThumbnailPath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FilePath", template.FilePath ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CssPath", template.CssPath ?? "");
                cmd.Parameters.AddWithValue("@ConfigPath", template.ConfigPath ?? "");
                cmd.Parameters.AddWithValue("@IsActive", template.IsActive);
                cmd.Parameters.AddWithValue("@Id", template.Id);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTemplate(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Templates WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Template MapTemplate(SqlDataReader reader)
        {
            return new Template
            {
                Id = (int)reader["Id"],
                Name = reader["TemplateName"].ToString(),
                Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                ThumbnailPath = reader["ThumbnailPath"] != DBNull.Value ? reader["ThumbnailPath"].ToString() : null,
                FilePath = reader["HtmlPath"].ToString(),
                CssPath = reader["CssPath"] != DBNull.Value ? reader["CssPath"].ToString() : null,
                ConfigPath = reader["ConfigPath"] != DBNull.Value ? reader["ConfigPath"].ToString() : null,
                IsActive = reader["IsActive"] != DBNull.Value ? (bool)reader["IsActive"] : true,
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime)reader["CreatedAt"] : DateTime.Now
            };
        }

        // --- Portfolio Methods ---

        /// <summary>
        /// Creates a new Portfolio row and returns its Id (SCOPE_IDENTITY)
        /// </summary>
        public int CreatePortfolio(int userId, int templateId, string title, string aboutMe)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Portfolios (UserId, TemplateId, Title, AboutMe) VALUES (@UserId, @TemplateId, @Title, @AboutMe); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@TemplateId", templateId);
                cmd.Parameters.AddWithValue("@Title", title ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AboutMe", aboutMe ?? (object)DBNull.Value);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public List<Portfolio> GetPortfoliosByUserId(int userId)
        {
            List<Portfolio> portfolios = new List<Portfolio>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Portfolios WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        portfolios.Add(MapPortfolio(reader));
                    }
                }
            }
            return portfolios;
        }

        public Portfolio GetPortfolioById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM Portfolios WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapPortfolio(reader);
                    }
                }
                return null;
            }
        }

        public void DeletePortfolio(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Portfolios WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Portfolio MapPortfolio(SqlDataReader reader)
        {
            return new Portfolio
            {
                Id = (int)reader["Id"],
                UserId = (int)reader["UserId"],
                TemplateId = (int)reader["TemplateId"],
                Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : null,
                AboutMe = reader["AboutMe"] != DBNull.Value ? reader["AboutMe"].ToString() : null,
                CreatedAt = reader["CreatedAt"] != DBNull.Value ? (DateTime)reader["CreatedAt"] : DateTime.Now
            };
        }

        // --- PortfolioPersonalInfo ---

        public void AddPortfolioPersonalInfo(PortfolioPersonalInfo info)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PortfolioPersonalInfo (PortfolioId, FullName, Profession, Email, Phone, Location) VALUES (@PId, @FullName, @Profession, @Email, @Phone, @Location)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", info.PortfolioId);
                cmd.Parameters.AddWithValue("@FullName", info.FullName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Profession", info.Profession ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", info.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", info.Phone ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Location", info.Location ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public PortfolioPersonalInfo GetPersonalInfoByPortfolioId(int portfolioId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM PortfolioPersonalInfo WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PortfolioPersonalInfo
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null,
                            Profession = reader["Profession"] != DBNull.Value ? reader["Profession"].ToString() : null,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                            Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : null,
                            Location = reader["Location"] != DBNull.Value ? reader["Location"].ToString() : null
                        };
                    }
                }
                return null;
            }
        }

        // --- Skills ---

        public void AddSkill(Skill skill)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Skills (PortfolioId, SkillName, SkillLevel) VALUES (@PId, @Name, @Level)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", skill.PortfolioId);
                cmd.Parameters.AddWithValue("@Name", skill.SkillName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Level", skill.SkillLevel ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Skill> GetSkillsByPortfolioId(int portfolioId)
        {
            var list = new List<Skill>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Skills WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Skill
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            SkillName = reader["SkillName"] != DBNull.Value ? reader["SkillName"].ToString() : null,
                            SkillLevel = reader["SkillLevel"] != DBNull.Value ? reader["SkillLevel"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        // --- Projects ---

        public void AddProject(Project project)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Projects (PortfolioId, ProjectTitle, Description, TechStack, GitHubLink, LiveLink) VALUES (@PId, @Title, @Desc, @Tech, @GH, @Live)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", project.PortfolioId);
                cmd.Parameters.AddWithValue("@Title", project.ProjectTitle ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Desc", project.Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Tech", project.TechStack ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@GH", project.GitHubLink ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Live", project.LiveLink ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Project> GetProjectsByPortfolioId(int portfolioId)
        {
            var list = new List<Project>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Projects WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Project
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            ProjectTitle = reader["ProjectTitle"] != DBNull.Value ? reader["ProjectTitle"].ToString() : null,
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            TechStack = reader["TechStack"] != DBNull.Value ? reader["TechStack"].ToString() : null,
                            GitHubLink = reader["GitHubLink"] != DBNull.Value ? reader["GitHubLink"].ToString() : null,
                            LiveLink = reader["LiveLink"] != DBNull.Value ? reader["LiveLink"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        // --- Experiences ---

        public void AddExperience(Experience exp)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Experiences (PortfolioId, CompanyName, Role, StartDate, EndDate, Description) VALUES (@PId, @Company, @Role, @Start, @End, @Desc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", exp.PortfolioId);
                cmd.Parameters.AddWithValue("@Company", exp.CompanyName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Role", exp.Role ?? (object)DBNull.Value);

                // StartDate and EndDate are SQL [date] columns — parse safely
                DateTime parsedStart;
                if (!string.IsNullOrEmpty(exp.StartDate) && DateTime.TryParse(exp.StartDate, out parsedStart))
                    cmd.Parameters.AddWithValue("@Start", parsedStart);
                else
                    cmd.Parameters.AddWithValue("@Start", DBNull.Value);

                DateTime parsedEnd;
                if (!string.IsNullOrEmpty(exp.EndDate) && DateTime.TryParse(exp.EndDate, out parsedEnd))
                    cmd.Parameters.AddWithValue("@End", parsedEnd);
                else
                    cmd.Parameters.AddWithValue("@End", DBNull.Value);

                cmd.Parameters.AddWithValue("@Desc", exp.Description ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Experience> GetExperiencesByPortfolioId(int portfolioId)
        {
            var list = new List<Experience>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Experiences WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Experience
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            CompanyName = reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : null,
                            Role = reader["Role"] != DBNull.Value ? reader["Role"].ToString() : null,
                            StartDate = reader["StartDate"] != DBNull.Value ? reader["StartDate"].ToString() : null,
                            EndDate = reader["EndDate"] != DBNull.Value ? reader["EndDate"].ToString() : null,
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        // --- Social Links ---

        public void AddSocialLink(SocialLink link)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO SocialLinks (PortfolioId, Platform, Url) VALUES (@PId, @Platform, @Url)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", link.PortfolioId);
                cmd.Parameters.AddWithValue("@Platform", link.Platform ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Url", link.Url ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<SocialLink> GetSocialLinksByPortfolioId(int portfolioId)
        {
            var list = new List<SocialLink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM SocialLinks WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SocialLink
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            Platform = reader["Platform"] != DBNull.Value ? reader["Platform"].ToString() : null,
                            Url = reader["Url"] != DBNull.Value ? reader["Url"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        // --- Education ---

        public void AddEducation(Education edu)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Education (PortfolioId, Degree, Institution, Year) VALUES (@PId, @Degree, @Institution, @Year)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", edu.PortfolioId);
                cmd.Parameters.AddWithValue("@Degree", edu.Degree ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Institution", edu.Institution ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Year", edu.Year ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Education> GetEducationByPortfolioId(int portfolioId)
        {
            var list = new List<Education>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Education WHERE PortfolioId = @PId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PId", portfolioId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Education
                        {
                            Id = (int)reader["Id"],
                            PortfolioId = (int)reader["PortfolioId"],
                            Degree = reader["Degree"] != DBNull.Value ? reader["Degree"].ToString() : null,
                            Institution = reader["Institution"] != DBNull.Value ? reader["Institution"].ToString() : null,
                            Year = reader["Year"] != DBNull.Value ? reader["Year"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        // --- OTP Verification Methods ---

        public void SaveOtp(string email, string code, string purpose)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Expire any existing un-used OTPs for this user and purpose
                string expireQuery = "UPDATE Otps SET IsUsed = 1 WHERE Email = @Email AND Purpose = @Purpose AND IsUsed = 0";
                SqlCommand expireCmd = new SqlCommand(expireQuery, conn);
                expireCmd.Parameters.AddWithValue("@Email", email);
                expireCmd.Parameters.AddWithValue("@Purpose", purpose);
                
                string insertQuery = "INSERT INTO Otps (Email, Code, Purpose, ExpiresAt, IsUsed) VALUES (@Email, @Code, @Purpose, @ExpiresAt, 0)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@Email", email);
                insertCmd.Parameters.AddWithValue("@Code", code);
                insertCmd.Parameters.AddWithValue("@Purpose", purpose);
                insertCmd.Parameters.AddWithValue("@ExpiresAt", DateTime.Now.AddMinutes(5));

                conn.Open();
                expireCmd.ExecuteNonQuery();
                insertCmd.ExecuteNonQuery();
            }
        }

        public bool VerifyOtp(string email, string code, string purpose)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM Otps WHERE Email = @Email AND Purpose = @Purpose AND IsUsed = 0 AND ExpiresAt > GETDATE() ORDER BY CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Purpose", purpose);
                
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var dbCode = reader["Code"].ToString();
                        var id = (int)reader["Id"];
                        if (dbCode == code)
                        {
                            reader.Close();
                            // Mark as used
                            string updateQuery = "UPDATE Otps SET IsUsed = 1 WHERE Id = @Id";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@Id", id);
                            updateCmd.ExecuteNonQuery();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // --- Pending Registrations Methods ---

        public void AddPendingRegistration(string fullName, string email, string passwordHash)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO PendingRegistrations (FullName, Email, PasswordHash, CreatedAt) VALUES (@FullName, @Email, @PasswordHash, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public PendingRegistration GetLatestPendingRegistration(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT TOP 1 * FROM PendingRegistrations WHERE Email = @Email ORDER BY CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new PendingRegistration
                        {
                            Id = (int)reader["Id"],
                            FullName = reader["FullName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        };
                    }
                }
            }
            return null;
        }

        public void DeletePendingRegistration(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM PendingRegistrations WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // --- Feedback Methods ---

        public void AddFeedback(Feedback feedback)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Feedback (UserId, TemplateId, PortfolioId, Rating, Message, CreatedAt) VALUES (@UserId, @TemplateId, @PortfolioId, @Rating, @Message, @CreatedAt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", feedback.UserId);
                cmd.Parameters.AddWithValue("@TemplateId", feedback.TemplateId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@PortfolioId", feedback.PortfolioId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                cmd.Parameters.AddWithValue("@Message", feedback.Message ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", feedback.CreatedAt == DateTime.MinValue ? DateTime.Now : feedback.CreatedAt);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Feedback> GetAllFeedback()
        {
            List<Feedback> feedbackList = new List<Feedback>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // Join with Users and Templates to get names
                string query = @"
                    SELECT f.*, u.FullName as UserName, t.TemplateName 
                    FROM Feedback f
                    LEFT JOIN Users u ON f.UserId = u.Id
                    LEFT JOIN Templates t ON f.TemplateId = t.Id
                    ORDER BY f.CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var feedback = MapFeedback(reader);
                        feedback.UserName = reader["UserName"].ToString();
                        feedback.TemplateName = reader["TemplateName"] != DBNull.Value ? reader["TemplateName"].ToString() : "N/A";
                        feedbackList.Add(feedback);
                    }
                }
            }
            return feedbackList;
        }

        private Feedback MapFeedback(SqlDataReader reader)
        {
            return new Feedback
            {
                Id = (int)reader["Id"],
                UserId = (int)reader["UserId"],
                TemplateId = reader["TemplateId"] != DBNull.Value ? (int?)reader["TemplateId"] : null,
                PortfolioId = reader["PortfolioId"] != DBNull.Value ? (int?)reader["PortfolioId"] : null,
                Rating = reader["Rating"] != DBNull.Value ? (int)reader["Rating"] : 0,
                Message = reader["Message"] != DBNull.Value ? reader["Message"].ToString() : null,
                CreatedAt = (DateTime)reader["CreatedAt"]
            };
        }

        // --- Notification Methods ---

        public void AddNotification(Notification notification)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Notifications (UserId, Message, IsRead, CreatedAt) VALUES (@UserId, @Message, @IsRead, @CreatedAt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", notification.UserId);
                cmd.Parameters.AddWithValue("@Message", notification.Message);
                cmd.Parameters.AddWithValue("@IsRead", notification.IsRead);
                cmd.Parameters.AddWithValue("@CreatedAt", notification.CreatedAt == DateTime.MinValue ? DateTime.Now : notification.CreatedAt);
                
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Notification> GetNotificationsByUserId(int userId)
        {
            var notifications = new List<Notification>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Notifications WHERE UserId = @UserId ORDER BY CreatedAt DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        notifications.Add(new Notification
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            Message = reader["Message"].ToString(),
                            IsRead = (bool)reader["IsRead"],
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        });
                    }
                }
            }
            return notifications;
        }

        public void MarkNotificationAsRead(int notificationId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Notifications SET IsRead = 1 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", notificationId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void MarkAllNotificationsAsRead(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Notifications SET IsRead = 1 WHERE UserId = @UserId AND IsRead = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetUnreadNotificationCount(int userId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Notifications WHERE UserId = @UserId AND IsRead = 0";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                conn.Open();
                // Ensure we handle possible null return or cast errors
                var result = cmd.ExecuteScalar();
                return result != null ? (int)result : 0;
            }
        }
        // --- Admin Dashboard Stats Methods ---

        public AdminDashboardViewModel GetAdminDashboardStats()
        {
            AdminDashboardViewModel stats = new AdminDashboardViewModel();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT 
                        (SELECT COUNT(*) FROM Users WHERE Role <> 'Admin') as TotalUsers,
                        (SELECT COUNT(*) FROM Portfolios) as TotalPortfolios,
                        (SELECT COUNT(*) FROM Templates) as TotalTemplates,
                        (SELECT COALESCE(AVG(CAST(Rating AS FLOAT)), 0) FROM Feedback) as AverageRating";
                
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stats.TotalUsers = (int)reader["TotalUsers"];
                        stats.TotalPortfolios = (int)reader["TotalPortfolios"];
                        stats.TotalTemplates = (int)reader["TotalTemplates"];
                        stats.AverageRating = Convert.ToDouble(reader["AverageRating"]);
                    }
                }
            }

            stats.TemplateUsage = GetTemplateUsageStats();
            stats.DailyPortfolios = GetDailyPortfolioStats(14);
            stats.DailyRegistrations = GetDailyRegistrationStats(14);

            return stats;
        }

        private List<TemplateUsageStat> GetTemplateUsageStats()
        {
            var list = new List<TemplateUsageStat>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT t.TemplateName, COUNT(p.Id) as UsageCount
                    FROM Templates t
                    LEFT JOIN Portfolios p ON t.Id = p.TemplateId
                    GROUP BY t.TemplateName";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TemplateUsageStat
                        {
                            TemplateName = reader["TemplateName"].ToString(),
                            UsageCount = (int)reader["UsageCount"]
                        });
                    }
                }
            }
            return list;
        }

        private List<DailyStat> GetDailyPortfolioStats(int days)
        {
            var list = new List<DailyStat>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT CAST(CreatedAt AS DATE) as [Date], COUNT(*) as [Count]
                    FROM Portfolios
                    WHERE CreatedAt >= DATEADD(day, -@Days, GETDATE())
                    GROUP BY CAST(CreatedAt AS DATE)
                    ORDER BY [Date]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Days", days);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DailyStat
                        {
                            Date = (DateTime)reader["Date"],
                            Count = (int)reader["Count"]
                        });
                    }
                }
            }
            return list;
        }

        private List<DailyStat> GetDailyRegistrationStats(int days)
        {
            var list = new List<DailyStat>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT CAST(CreatedAt AS DATE) as [Date], COUNT(*) as [Count]
                    FROM Users
                    WHERE Role <> 'Admin' AND CreatedAt >= DATEADD(day, -@Days, GETDATE())
                    GROUP BY CAST(CreatedAt AS DATE)
                    ORDER BY [Date]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Days", days);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DailyStat
                        {
                            Date = (DateTime)reader["Date"],
                            Count = (int)reader["Count"]
                        });
                    }
                }
            }
            return list;
        }
    }

    public class PendingRegistration
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
