
using System.Collections.Generic;

namespace Capstone.Web
{
    public class DashboardStudentViewModel
    {
        public List<Course> EnrolledCourses { get; set; }
        public List<Course> AvailCourses { get; set; }
        public List<User> Users { get; set; }
    }
}