
using System.Collections.Generic;

namespace Capstone.Web
{
    public class DashboardTeacherViewModel
    {
        public List<Course> Courses { get; set; }
        public List<Course> CoursesWithPic { get; set; }
        public List<User> Users { get; set; }
        public List<User> AllUsers { get; set; }
        public List<StudentAssignment> Assignments { get; set; }
    }
}