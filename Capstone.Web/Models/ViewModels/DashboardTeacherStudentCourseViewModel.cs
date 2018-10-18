using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web
{
    public class DashboardTeacherStudentCourseViewModel
    {
        public List<User> Students { get; set; }
        public Course Course { get; set; }
    }
}