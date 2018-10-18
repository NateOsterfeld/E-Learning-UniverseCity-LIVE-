using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models.Data;

namespace Capstone.Web
{
    public class MockDatabaseDAL : IDatabaseDAL
    {
        //create dummy data
        private Dictionary<int, Role> mockRoles = new Dictionary<int, Role>();
        private Dictionary<int, User> mockUsers = new Dictionary<int, User>();
        private Dictionary<int, Course> mockCourses = new Dictionary<int, Course>();
        
        public MockDatabaseDAL()
        {
            mockRoles.Add(1, new Role() { RoleId = 1, RoleName = "teacher" });
            mockRoles.Add(2, new Role() { RoleId = 2, RoleName = "student" });
        }

        public int CreateUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetCourses()
        {
            return mockCourses.Values.ToList();
        }

        public bool CreateCourse()
        {
            throw new NotImplementedException();
        }

        public User GetTeacherForCourse(int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetCoursesForTeacher(int teacherId)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public void CreateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        public Assignment CreateAssignment(Assignment assignment)
        {
            throw new NotImplementedException();
        }


        public List<User> GetUsersForTeacher(int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<StudentAssignment> GetStudentAssignmentsForTeacher(int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersForAssignment(int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<Assignment> GetAssignmentsForACourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAssignment(int assignmentId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersForCourse(int CourseId)
        {
            throw new NotImplementedException();
        }



        public List<Course> GetCoursesForStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public void CreateFileForTeacherAssignment(HttpPostedFileBase postedFile, int AssignmentId)
        {
            throw new NotImplementedException();
        }

        void IDatabaseDAL.CreateFileForTeacherAssignment(HttpPostedFileBase postedFile, int AssignmentId)
        {
            throw new NotImplementedException();
        }


        public void UpdateAssignmentWithFileId(int AssignmentId)
        {
            throw new NotImplementedException();
        }


        public void EnrollStudentInCourse(int userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetAvailableCourses(int userId)
        {
            throw new NotImplementedException();
        }

        public List<CombinedAssignment> GetCombinedAssignments(int courseId, int userId)
        {
            throw new NotImplementedException();
        }

        public void EnrollStudent(int userId, int courseId)
        {
            throw new NotImplementedException();
        }

        public List<Assignment> GetAssignmentsWithVideoForACourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public void CreateFileForTeacherCourse(HttpPostedFileBase postedFile, int courseId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourseWithFileId(int courseId)
        {
            throw new NotImplementedException();
        }

        public List<Course> GetCoursesWithPicForATeacher(int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Course GetCourseId(string courseName)
        {
            throw new NotImplementedException();
        }

        Course IDatabaseDAL.CreateCourse(Course course)
        {
            throw new NotImplementedException();
        }

        ImageFile IDatabaseDAL.CreateFileForTeacherCourse(HttpPostedFileBase postedFile, int courseId)
        {
            throw new NotImplementedException();
        }

        Assignment IDatabaseDAL.GetAssignmentId(string assignmentName)
        {
            throw new NotImplementedException();
        }

        public List<Course> SearchCourses(string search)
        {
            throw new NotImplementedException();
        }
    }
}