using Capstone.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Capstone.Web
{
    public class DatabaseDAL : IDatabaseDAL
    {
        private readonly string databaseConnectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        public DatabaseDAL(string connectionString)
        {
            databaseConnectionString = connectionString;
        }

        /// <summary>
        /// Changes the password for a user.
        /// </summary>
        /// <param name="email">email / username for the user you want to change the password for</param>
        /// <param name="newPassword"> New hashed password</param>
        /// <returns></returns>
        public bool ChangePassword(string email, string newPassword)
        {
            try
            {
                string sql = $"UPDATE User SET Password = '{newPassword}' WHERE Email = '{email}'";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new user in the DB
        /// </summary>
        /// <param name="newUser"> User to add to the DB</param>
        /// <returns>Returns true of it was succesfull and false if it was not.</returns>
        public int CreateUser(User newUser) //using viewmodel
        {
            int result = 0;

            //salt
            string sql = $"INSERT INTO [User] (FirstName, LastName, Password, Email, RoleId, Salt)" +
                                       $" VALUES (@FirstName, @LastName, @Password, @Email, @RoleId, @Salt);";

            using (SqlConnection conn = new SqlConnection(databaseConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FirstName", newUser.FirstName);
                cmd.Parameters.AddWithValue("@LastName", newUser.LastName);
                cmd.Parameters.AddWithValue("@Password", newUser.HashedPassword);
                cmd.Parameters.AddWithValue("@Email", newUser.Email);
                cmd.Parameters.AddWithValue("@RoleId", newUser.RoleId);
                cmd.Parameters.AddWithValue("@Salt", newUser.Salt);

                result = (int)cmd.ExecuteScalar();
            }

            return result;
        }

        /// <summary>
        /// 
        /// 
        ///  NEEDS DOCUMENTATION / IMPLEMENTATION
        /// 
        /// 
        /// </summary>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public List<string> GetUsernames(string startsWith)
        {
            List<string> usernames = new List<string>();

            try
            {
                string sql = $"SELECT Email FROM User WHERE Email LIKE '{startsWith}%';";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usernames.Add(Convert.ToString(reader["user_name"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return usernames;
        }

        /// <summary>
        /// gets a specific user from the database
        /// </summary>
        /// <param name="email"> email / username of the user you want to get</param>
        /// <returns>user</returns>
        public User GetUser(string email)
        {
            User user = null;

            try
            {
                string sql = $"SELECT [UserId], [FirstName], [LastName], [Email], [Password], [Salt], [RoleId] FROM [User] WHERE Email = @email";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["password"]),
                            Salt = Convert.ToString(reader["Salt"]),
                        };
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                string sql = $"SELECT * FROM [USER]";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User tempUser = new User
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            RoleId = Convert.ToInt32(reader["RoleId"]),
                            Email = Convert.ToString(reader["Email"]),
                            Password = Convert.ToString(reader["password"]),
                            Salt = Convert.ToString(reader["Salt"]),
                        };
                        users.Add(tempUser);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return users;
        }

        /// <summary>
        /// Gets the roles from the DB
        /// </summary>
        /// <returns>List of Roles</returns>
        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();

            try
            {
                string sql = $"SELECT * FROM [Role]";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Role role = PopulateRolesFromReader(reader);
                        roles.Add(role);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return roles;
        }

        private Role PopulateRolesFromReader(SqlDataReader reader)
        {
            Role item = new Role
            {
                RoleId = Convert.ToInt32(reader["RoleId"]),
                RoleName = Convert.ToString(reader["RoleName"]),
            };
            return item;
        }

        private User PopulateTeachersFromReader(SqlDataReader reader)
        {
            User item = new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Email = Convert.ToString(reader["Email"])
            };
            return item;
        }

        /// <summary>
        /// Gets a list of all the courses in the DB
        /// </summary>
        /// <returns>List of courses</returns>
        public List<Course> GetCourses()
        {
            List<Course> courses = new List<Course>();

            try
            {
                string sql = "SELECT [Course].CourseId, [Course].CourseName, [Course].Difficulty, [Course].CourseFileId, [Course].Description, [Course].CostUSD, [Course].TeacherId, [Course].CourseRating, [CourseFile].Name, [CourseFile].FileSize, [CourseFile].FilePath " +
                             "FROM [Course] " +
                             "JOIN [CourseFile] ON [Course].CourseId = [CourseFile].CourseId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        bool isRatingNull = Convert.IsDBNull(reader["CourseRating"]);
                        bool isImageNull = Convert.IsDBNull(reader["FilePath"]);
                        Course tempcourse = new Course();
                        if (isRatingNull && isImageNull)
                        {
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);

                        }
                        else if (!isRatingNull && !isImageNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }
                        else if (!isRatingNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                        }
                        else if (!isImageNull)
                        {

                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image = new ImageFile();
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }

                        courses.Add(tempcourse);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            Course[] resultArray = courses.ToArray();
            List<Course> result = TestCourse(resultArray);
            return result;
        }

        /// <summary>
        /// Gets a list of all the courses in the DB
        /// </summary>
        /// <returns>List of courses</returns>
        public List<Course> SearchCourses(string search)
        {
            List<Course> courses = new List<Course>();
            
            try
            {
                
                
                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(("Select [Course].CourseId, [Course].CourseName, [Course].Difficulty, [Course].CourseFileId, [Course].Description, [Course].TeacherId, [Course].CourseRating, [CourseFile].Name, [CourseFile].FileSize, [CourseFile].FilePath"
                                 + " from [Course] JOIN [CourseFile] ON [Course].CourseID = [CourseFile].CourseID"
                                 + " where (CourseName LIKE @search) OR (description LIKE @search)"), conn);
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        bool isRatingNull = Convert.IsDBNull(reader["CourseRating"]);
                        bool isImageNull = Convert.IsDBNull(reader["FilePath"]);
                        Course tempcourse = new Course();
                        if (isRatingNull && isImageNull)
                        {
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);

                        }
                        else if (!isRatingNull && !isImageNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }
                        else if (!isRatingNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                        }
                        else if (!isImageNull)
                        {

                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image = new ImageFile();
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }

                        courses.Add(tempcourse);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            Course[] resultArray = courses.ToArray();
            List<Course> result = TestCourse(resultArray);
            return result;
        }

        /// <summary>
        /// given an ID for a teacher, returns all courses for that teacher. 
        /// </summary>
        /// <param name="teacherId">id fo the teacher</param>
        /// <returns>all courses for a single teacher.</returns>
        public List<Course> GetCoursesForTeacher(int teacherId)
        {
            List<Course> courses = new List<Course>();

            try
            {
                string sql = $"SELECT CourseId, Description, CourseName, Difficulty, TeacherId, CourseRating, CostUSD " +
                             $"FROM [Course] " +
                             $"WHERE TeacherId = @teacherId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Course tempcourse = new Course
                        {
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            CourseName = Convert.ToString(reader["CourseName"]),
                            Description = Convert.ToString(reader["Description"]),
                            Difficulty = Convert.ToInt32(reader["Difficulty"]),
                            TeacherId = Convert.ToInt32(reader["TeacherID"]),
                            CostUSD = Convert.ToInt32(reader["CostUSD"])
                            //CourseRating = Convert.ToInt32(reader["CourseRating"])
                        };

                        courses.Add(tempcourse);
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Error getting courses for that teacher");
            }

            return courses;
        }


        public void EnrollStudent(int userId, int courseId)
        {
            {

                try
                {
                    string sql = $"INSERT INTO [StudentCourse] (UserId, CourseId) " +
                                 $"VALUES (@userId, @courseId)";

                    using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                    {
                        conn.Open();

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@courseId", courseId);

                        //send command
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            throw new Exception("ERROR: Student was not enrolled. ");
                        }
                    }
                }
                catch (SqlException)
                {
                    throw;
                }
            }
        }




        /// <summary>
        /// given an ID for a teacher, returns all courses for that teacher. 
        /// </summary>
        /// <param name="userid">id for the student</param>
        /// <returns>all courses for a single student.</returns>
        public List<Course> GetCoursesForStudent(int userId)
        {
            List<Course> courses = new List<Course>();

            try
            {
                string sql = $"SELECT [Course].CourseId, [Course].CourseName, [Course].Difficulty, [Course].CourseFileId, [Course].Description, [Course].CostUSD, [Course].TeacherId, [Course].CourseRating, [CourseFile].Name, [CourseFile].FileSize, [CourseFile].FilePath " +
                             $"FROM [Course] " +
                             $"JOIN StudentCourse ON Course.CourseId = StudentCourse.CourseId " +
                             $"JOIN CourseFile ON Course.CourseId = CourseFile.CourseId " +
                             $"WHERE StudentCourse.UserId = @UserId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        bool isRatingNull = Convert.IsDBNull(reader["CourseRating"]);
                        bool isImageNull = Convert.IsDBNull(reader["FilePath"]);
                        Course tempcourse = new Course();
                        if (isRatingNull && isImageNull)
                        {
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);

                        }
                        else if (!isRatingNull && !isImageNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }
                        else if (!isRatingNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                        }
                        else if (!isImageNull)
                        {

                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                            tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                            tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                            tempcourse.Image = new ImageFile();
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                        }
                        courses.Add(tempcourse);
                    }
                }
            }
            catch (SqlException)
            {
                throw new Exception("Error getting courses for that student");
            }

            Course[] resultArray = courses.ToArray();
            List<Course> result = TestCourse(resultArray);
            return result;
        }       

        /// <summary>
        /// Gets a specific course for a given courseID From the DB
        /// </summary>
        /// <param name="courseId">ID of the course you want to get. </param>
        /// <returns>Course</returns>
        public Course GetCourse(int courseId)
        {
            Course tempcourse = new Course();

            try
            {
                string sql = $"SELECT CourseId, Description, CourseName, Difficulty, TeacherId, CourseRating " +
                             $"FROM [Course] " +
                             $"WHERE CourseId = @courseId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool isRatingNull = Convert.IsDBNull(reader["CourseRating"]);                      
                        if (isRatingNull)
                        {
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                        }
                        else if (!isRatingNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return tempcourse;
        }

        public Course GetCourseId(string courseName)
        {
            Course tempcourse = new Course();

            try
            {
                string sql = $"SELECT CourseId, Description, CourseName, Difficulty, TeacherId, CourseRating " +
                             $"FROM [Course] " +
                             $"WHERE CourseName = @courseName";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@courseName", courseName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        bool isRatingNull = Convert.IsDBNull(reader["CourseRating"]);
                        if (isRatingNull)
                        {
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                        }
                        else if (!isRatingNull)
                        {
                            tempcourse.CourseRating = Convert.ToInt32(reader["CourseRating"]);
                            tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                            tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                            tempcourse.Description = Convert.ToString(reader["Description"]);
                            tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                            tempcourse.TeacherId = Convert.ToInt32(reader["TeacherID"]);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return tempcourse;
        }

        public Assignment GetAssignmentId(string assignmentName)
        {
            Assignment tempAssignment = new Assignment();

            try
            {
                string sql = $"SELECT AssignmentId, AssignmentName, Instructions, CourseId " +
                             $"FROM [Assignment] " +
                             $"WHERE AssignmentName = @assignmentName";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@assignmentName", assignmentName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tempAssignment.AssignmentId = Convert.ToInt32(reader["AssignmentId"]);
                        tempAssignment.AssignmentName = Convert.ToString(reader["AssignmentName"]);
                        tempAssignment.Instructions = Convert.ToString(reader["Instructions"]);
                        tempAssignment.CourseId = Convert.ToInt32(reader["CourseId"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return tempAssignment;
        }

        /// <summary>
        /// Adds a course to the DB
        /// </summary>
        /// <param name="course"> Course to add to the DB</param>
        public Course CreateCourse(Course course)
        {
            Course newCourse = new Course();

            try
            {
                string sql = $"INSERT INTO [course] (Description, CourseName, Difficulty, TeacherId, CostUSD) " +
                             $"VALUES (@description, @courseName, @difficulty, @teacherId, @costusd)";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@description", course.Description);
                    newCourse.Description = course.Description;
                    cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                    newCourse.CourseName = course.CourseName;
                    cmd.Parameters.AddWithValue("@difficulty", course.Difficulty);
                    newCourse.Difficulty = course.Difficulty;
                    cmd.Parameters.AddWithValue("@teacherId", course.TeacherId);
                    newCourse.TeacherId = course.TeacherId;
                    cmd.Parameters.AddWithValue("@costusd", course.CostUSD);
                    newCourse.CostUSD = course.CostUSD;

                    //send command
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("ERROR: Course was not added. ");
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return newCourse;
        }

        /// <summary>
        /// Adds an assignment to the DB
        /// </summary>
        /// <param name="assignment">Assignment to be added to the DB</param>
        public Assignment CreateAssignment(Assignment assignment)
        {
            Assignment newAssignmnet = new Assignment();
            try
            {
                string sql = $"INSERT INTO [Assignment] (Instructions, CourseId, AssignmentName)" +
                    $" VALUES (@instructions, @courseId, @assignmentName)";

                //string sql = $"INSERT INTO [Assignment] (AssignmentName, Instructions, CourseId)" +
                //    $" VALUES (@assignmentName, @instructions, @courseId)";

                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@assignmentName", assignment.AssignmentName);
                    newAssignmnet.AssignmentName = assignment.AssignmentName;
                    command.Parameters.AddWithValue("@instructions", assignment.Instructions);
                    newAssignmnet.Instructions = assignment.Instructions;
                    command.Parameters.AddWithValue("@courseId", assignment.CourseId);
                    newAssignmnet.CourseId = assignment.CourseId;
                    newAssignmnet.AssignmentId = assignment.AssignmentId;

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception("ERROR: assignment was not added.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return newAssignmnet;
        }




        /// <summary>
        /// gets the teacher for a specific course.
        /// </summary>
        /// <param name="teacherId"> Id of the teacher you want to for the course </param>
        /// <returns> Returns a User (teacher) for a specific course.</returns>
        public User GetTeacherForCourse(int teacherId)
        {

            User teacher = new User();

            try
            {
                string sql = $"SELECT UserId, FirstName, LastName, Email, RoleId" +
                                $" FROM [User]" +
                                $" JOIN [Course]" +
                                $" ON [User].UserId = [Course].TeacherId" +
                                $" WHERE [Course].TeacherId = @teacherId";                                    

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        teacher = PopulateTeachersFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return teacher;
        }

        /// <summary>
        /// gets all the students taking courses by a single teacher.
        /// </summary>
        /// <param name="teacherId"> Id of the teacher you want to get users for </param>
        /// <returns> Returns a List of studnets for a teacher.</returns>
        public List<User> GetUsersForTeacher(int teacherId)
        {

            List<User> users = new List<User>();

            try
            {
                string sql = $"SELECT [UserId], [FirstName], [LastName], [Email], [RoleId] " +
                                $"FROM[User] " +
                                $"WHERE UserId IN(SELECT[TeacherID] " +
                                    $"FROM[Course] " +
                                    $"WHERE TeacherID = @teacherId);";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            Email = Convert.ToString(reader["Email"]),
                            RoleId = Convert.ToInt32(reader["RoleId"])
                        };
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return users;
        }

        /// <summary>
        /// Gets all the student assignments for a specific teacher
        /// </summary>
        /// <param name="teacherId">Id of the teacher you want to get assignments for.</param>
        /// <returns>Returns a list of studnet assignemnts</returns>
        public List<StudentAssignment> GetStudentAssignmentsForTeacher(int teacherId)
        {

            List<StudentAssignment> assignments = new List<StudentAssignment>();

            try
            {
                string sql = $"SELECT [StudentAssignmentId], [AssignmentId], [FileID], [AssignmentName], [IsCompleated], [Grade], [TeacherComments]" +
                                "FROM[StudentAssignment]" +
                                "WHERE[StudentAssignmentId] IN(SELECT[UserId]" +
                                                   "FROM[User]" +
                                                   "WHERE UserId IN(SELECT[TeacherID]" +
                                                                   "FROM[Course]" +
                                                                   "WHERE TeacherID = @teacherId)); ";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentAssignment assignment = new StudentAssignment
                        {
                            StudentId = Convert.ToInt32(reader["StudentAssignmentId"]),
                            AssignmentId = Convert.ToInt32(reader["AssignmentId"]),
                            FileId = Convert.ToInt32(reader["FileID"]),
                            Grade = (float)Convert.ToDouble(reader["Grade"]),
                            TeacherComments = Convert.ToString(reader["TeacherComments"]),
                            AssignmentName = Convert.ToString(reader["AssignmentName"])
                            
                        };
                        assignments.Add(assignment);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return assignments;
        }

        /// <summary>
        /// Gets all of the students for a given course.
        /// </summary>
        /// <param name="CourseId">Id of the course you want to get the users for</param>
        /// <returns>List of students</returns>
        public List<User> GetUsersForAssignment(int CourseId)
        {
            List<User> users = new List<User>();

            try
            {
                string sql = "SELECT [UserId],[FirstName],[LastName],[Email],[RoleId] " +
                             "FROM [User] " +
                             "Where [User].UserId IN " +
                                "(SELECT [UserId] " +
                                "FROM [StudentCourse] " +
                                "WHERE [StudentCourse].CourseId = @CourseId)";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CourseId", CourseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User tempUser = new User
                        {
                            Email = Convert.ToString(reader["Email"]),
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            RoleId = Convert.ToInt32(reader["RoleId"])
                        };

                        users.Add(tempUser);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return users;
        }

        /// <summary>
        /// Get the assignments for a specific course.
        /// </summary>
        /// <param name="courseId"> Id of the course you want to get assignemnts for.</param>
        /// <returns> Returns a list of assignemnts </returns>
        public List<Assignment> GetAssignmentsForACourse(int courseId)
        {
            List<Assignment> assignments = new List<Assignment>();    

            try
            {
                string sql = "SELECT AssignmentId, AssignmentName, Instructions, CourseId FROM[Assignment] Where CourseId = @courseId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Assignment tempassignment = new Assignment
                        {
                            AssignmentId = Convert.ToInt32(reader["AssignmentId"]),
                            AssignmentName = Convert.ToString(reader["AssignmentName"]),
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            Instructions = Convert.ToString(reader["Instructions"])
                        };

                        assignments.Add(tempassignment);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return assignments;
        }

        public List<Assignment> GetAssignmentsWithVideoForACourse(int courseId)
        {
            List<Assignment> assignments = new List<Assignment>();

            try
            {
                string sql = "SELECT [Assignment].AssignmentId, [Assignment].AssignmentName, [Assignment].FileId, [Assignment].Instructions, [Assignment].CourseId, [File].Name, [File].FileSize, [File].FilePath " +
                             "FROM[Assignment] JOIN[FILE] ON[Assignment].AssignmentId = [File].AssignmentId " +
                             "WHERE CourseId = @courseId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Assignment tempassignment = new Assignment
                        {
                            AssignmentId = Convert.ToInt32(reader["AssignmentId"]),
                            AssignmentName = Convert.ToString(reader["AssignmentName"]),
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            Instructions = Convert.ToString(reader["Instructions"])
                        };
                        if (reader["FileId"] != DBNull.Value)
                        {
                            tempassignment.Video = new VideoFile();
                            tempassignment.Video.ID = Convert.ToInt32(reader["FileId"]);
                            tempassignment.Video.Name = Convert.ToString(reader["Name"]);
                            tempassignment.Video.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempassignment.Video.FilePath = Convert.ToString(reader["FilePath"]);
                        }
                        assignments.Add(tempassignment);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            Assignment[] resultArray = assignments.ToArray();
            List<Assignment>result = Test(resultArray);
            return result;
        }

        public List<Course> GetCoursesWithPicForATeacher(int teacherId)
        {
            List<Course> courses = new List<Course>();

            try
            {
                string sql = "SELECT [Course].CourseId, [Course].CourseName, [Course].CourseFileId, [Course].Description, [Course].Difficulty, [Course].CostUSD, [Course].TeacherId, [CourseFile].Name, [CourseFile].FileSize, [CourseFile].FilePath " +
                             "FROM[Course] JOIN[CourseFILE] ON[Course].CourseId = [CourseFile].CourseId " +
                             "WHERE TeacherId = @teacherId";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@teacherId", teacherId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Course tempcourse = new Course();                      
                        tempcourse.CourseId = Convert.ToInt32(reader["CourseId"]);
                        tempcourse.CourseName = Convert.ToString(reader["CourseName"]);
                        tempcourse.TeacherId = Convert.ToInt32(reader["TeacherId"]);
                        tempcourse.Description = Convert.ToString(reader["Description"]);
                        tempcourse.CourseFileId = Convert.ToInt32(reader["CourseFileId"]);
                        tempcourse.CostUSD = Convert.ToInt32(reader["CostUSD"]);
                        tempcourse.Difficulty = Convert.ToInt32(reader["Difficulty"]);
                        
                        if (reader["FilePath"] != DBNull.Value)
                        {
                            tempcourse.Image = new ImageFile();
                            tempcourse.Image.Name = Convert.ToString(reader["Name"]);
                            tempcourse.Image.FileSize = Convert.ToInt32(reader["FileSize"]);
                            tempcourse.Image.FilePath = Convert.ToString(reader["FilePath"]);
                        }
                        courses.Add(tempcourse);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            Course[] resultArray = courses.ToArray();
            List<Course> result = TestCourse(resultArray);
            return result;
        }
        /// <summary>
        /// Deletes an assignement
        /// </summary>
        /// <param name="assignmentId"> Id of the assignment you want to delete</param>
        public void DeleteAssignment(int assignmentId)
        {

            try
            {
                string sql = "DELETE FROM[Assignment] WHERE AssignmentId = @assignmentId;";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@assignmentId", assignmentId);
                    int rowsEffected = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets a list of users for a given course id.
        /// </summary>
        /// <param name="courseId"> Id of the course you want to get</param>
        /// <returns> Returns a list of users</returns>
        public List<User> GetUsersForCourse(int courseId)
        {
            List<User> users = new List<User>();

            try
            {
                string sql = "SELECT [UserId],[FirstName],[LastName],[Email],[RoleId] FROM [User] WHERE [UserId] IN (SELECT UserId FROM StudentCourse WHERE CourseId = @courseId);";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User tempUser = new User
                        {
                            FirstName = Convert.ToString(reader["FirstName"]),
                            LastName = Convert.ToString(reader["LastName"]),
                            Email = Convert.ToString(reader["Email"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            RoleId = Convert.ToInt32(reader["RoleId"]),

                        };
                        users.Add(tempUser);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return users;

        }




        public ImageFile CreateFileForTeacherCourse(HttpPostedFileBase postedFile, int courseId)
        {
            ImageFile newImageFile = new ImageFile();
            if (postedFile != null)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                int fileSize = postedFile.ContentLength;
                int Size = fileSize / 1000;
                postedFile.SaveAs(HostingEnvironment.MapPath("~/ImageFileUpload/" + fileName));

                using (SqlConnection con = new SqlConnection(databaseConnectionString))
                {
                    string sql = "INSERT INTO [CourseFile] (Name, FileSize, FilePath, CourseId) VALUES (@Name, @FileSize, @FilePath, @CourseId)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", fileName);
                        newImageFile.Name = fileName;
                        cmd.Parameters.AddWithValue("@FileSize", Size);
                        newImageFile.FileSize = Size;
                        cmd.Parameters.AddWithValue("@FilePath", "~/ImageFileUpload/" + fileName);
                        newImageFile.FilePath = "~/ImageFileUpload/" + fileName;
                        cmd.Parameters.AddWithValue("@CourseId", courseId);
                        newImageFile.CourseId = courseId;
                        con.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("ERROR: assignment was not added.");
                        }
                    }
                }
            }
            return newImageFile;
        }

        public void CreateFileForTeacherAssignment(HttpPostedFileBase postedFile, int AssignmentId)
        {
            if (postedFile != null)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                int fileSize = postedFile.ContentLength;
                int Size = fileSize / 1000;
                postedFile.SaveAs(HostingEnvironment.MapPath("~/VideoFileUpload/" + fileName));

                //COULD EASILY BE DONE BY MAKING A DAL CLASS TO CLEAN UP

                using (SqlConnection con = new SqlConnection(databaseConnectionString))
                {
                    string sql = "INSERT INTO [File] (Name, FileSize, FilePath, AssignmentId) VALUES (@Name, @FileSize, @FilePath, @AssignmentId)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Name", fileName);
                        cmd.Parameters.AddWithValue("@FileSize", Size);
                        cmd.Parameters.AddWithValue("@FilePath", "~/VideoFileUpload/" + fileName);
                        cmd.Parameters.AddWithValue("@AssignmentId", AssignmentId);
                        con.Open();

                        var firstColumn = cmd.ExecuteNonQuery();

                        if (firstColumn == 0)
                        {
                            throw new Exception("Failed to add file");
                        }
                    }
                }
            }
        }

        public void UpdateAssignmentWithFileId(int AssignmentId)
        {

            using (SqlConnection conn = new SqlConnection(databaseConnectionString))
            {
                string sql = "UPDATE [Assignment] SET FileId = [File].fileId FROM [Assignment] JOIN [FILE] ON [Assignment].AssignmentId = [File].AssignmentId WHERE [Assignment].AssignmentId = @assignmentId";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@assignmentId", AssignmentId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void UpdateCourseWithFileId(int courseId)
        {

            using (SqlConnection conn = new SqlConnection(databaseConnectionString))
            {
                string sql = "UPDATE [Course] SET CourseFileId = [CourseFile].CourseFileId FROM [Course] JOIN [CourseFILE] ON [Course].CourseId = [CourseFile].CourseId WHERE [Course].CourseId = @courseId";

                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@courseId", courseId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

       

        public void EnrollStudentInCourse(int userId, int courseId)
        {
            try
            {
                string sql = $"INSERT INTO [StudentCourse] (UserId, CourseId)" +
                    $" VALUES (@userId, @courseId)";

                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@courseId", courseId);

                    //send command
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("ERROR: Student was not enrolled. ");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Course> GetAvailableCourses(int userId)
        {
            throw new NotImplementedException(); // not used?
        }

        public List<CombinedAssignment> GetCombinedAssignments(int courseId, int userId)
        {
            List<CombinedAssignment> comboAssignments = new List<CombinedAssignment>();

            try
            {
                string sql = "SELECT Assignment.AssignmentId, Assignment.AssignmentName, Assignment.OrderNumber, Assignment.FileId, Assignment.Instructions, Assignment.CourseId,StudentAssignment.StudentId, StudentAssignment.AssignmentId, StudentAssignment.FileID, StudentAssignment.Grade, StudentAssignment.TeacherComments FROM Assignment JOIN StudentAssignment ON Assignment.AssignmentId = StudentAssignment.AssignmentId WHERE StudentAssignment.StudentId = @studentId AND Assignment.CourseId = @courseId;";

                using (SqlConnection conn = new SqlConnection(databaseConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@studentId", userId);
                    cmd.Parameters.AddWithValue("@courseId", courseId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        //CombinedAssignment tempCombo = new CombinedAssignment
                        //{
                        //    AssignmentId = Convert.ToInt32(reader["AssignmentId"]),
                        //    AssignmentName = Convert.ToString(reader["AssignmentName"]),
                        //    CourseId = Convert.ToInt32(reader["CourseId"]),
                        //    Instructions = Convert.ToString(reader["Instructions"])
                        //};
                        //if (reader["FileId"] != DBNull.Value)
                        //{
                        //    tempassignment.FileId = Convert.ToInt32(reader["FileId"]);
                        //}
                        //assignments.Add(tempassignment);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return comboAssignments;
        }

        public List<Assignment> Test(Assignment[] array)
        {
            // Use nested loop to check for duplicates.
            List<Assignment>
                result = new List<Assignment>
                    ();
            for (int i = 0; i < array.Length; i++)
            {
                // Check for duplicates in all following elements.
                bool isDuplicate = false;
                for (int y = i + 1; y < array.Length; y++)
                {
                    if (array[i].AssignmentName == array[i + 1].AssignmentName)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    result.Add(array[i]);
                }
            }
            return result;
        }

        public List<Course> TestCourse(Course[] array)
        {
            // Use nested loop to check for duplicates.
            List<Course>
                result = new List<Course>
                    ();
            for (int i = 0; i < array.Length; i++)
            {
                // Check for duplicates in all following elements.
                bool isDuplicate = false;
                for (int y = i + 1; y < array.Length; y++)
                {
                    if (array[i].CourseName == array[i + 1].CourseName)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    result.Add(array[i]);
                }
            }
            return result;
        }

    }
}

