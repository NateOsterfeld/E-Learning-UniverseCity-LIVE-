
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Capstone.Web
{
    public class CreateAssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public int OrderNumber { get; set; }
        public int FileId { get; set; }
        public int CourseId { get; set; }
        public List<Course> CourseName { get; set; }

        [Required(ErrorMessage = "The Course Name is required")]
        [MaxLength(50, ErrorMessage = "The course name is too long. Please limit the name to 50 characters or less.")]
        [DataType(DataType.Text)]
        public string AssignmentName { get; set; }


        [Required(ErrorMessage = "Provide some instructions for your course")]
        [MaxLength(8000, ErrorMessage = "The course description is too long." +
                                        "Please limit the description to 140 characters or less.")]
        [DataType(DataType.Text)]
        public string Instructions { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase PostedFile { get; set; }

        /// <summary>
        /// key to get the current course id on the session
        /// </summary>
        public string CurrentCourseIdKey {
            get { return "CurrentCourseId"; }
        }
    }
}