using Capstone.Web.Models.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web
{
    public interface IDatabaseDAL
    {
        User GetUser(string email);
        List<User> GetAllUsers();
        User GetTeacherForCourse(int teacherId);
        int CreateUser(User newUser);
        bool ChangePassword(string username, string newPassword);
        List<Role> GetRoles();
        List<Course> GetCourses();
        List<Course> SearchCourses(string search);
        List<Course> GetCoursesForTeacher(int teacherId);
        List<Course> GetCoursesWithPicForATeacher(int teacherId);
        List<Course> GetCoursesForStudent(int userId);
        List<User> GetUsersForCourse(int CourseId);

        List<User> GetUsersForTeacher(int teacherId);
        List<User> GetUsersForAssignment(int teacherId);

        List<Assignment> GetAssignmentsForACourse(int courseId);
        List<Assignment> GetAssignmentsWithVideoForACourse(int courseId);
        void DeleteAssignment(int assignmentId);
        Assignment CreateAssignment(Assignment assignment);
        Assignment GetAssignmentId(string assignmentName);

        List<StudentAssignment> GetStudentAssignmentsForTeacher(int teacherId);
        Course GetCourse(int courseId);
        Course GetCourseId(string courseName);
        Course CreateCourse(Course course);
        void EnrollStudent(int userId, int courseId);


        void CreateFileForTeacherAssignment(HttpPostedFileBase postedFile, int AssignmentId);
        ImageFile CreateFileForTeacherCourse(HttpPostedFileBase postedFile, int courseId);
        void UpdateAssignmentWithFileId(int AssignmentId);
        void UpdateCourseWithFileId(int courseId);
        void EnrollStudentInCourse(int userId, int courseId);
        List<Course> GetAvailableCourses(int userId);
        List<CombinedAssignment> GetCombinedAssignments(int courseId, int userId);
        
    }
}
