
using Capstone.Web.Models.Data;

namespace Capstone.Web
{
    public class Assignment
    {
        public string AssignmentName { get; set; }
        public int AssignmentId { get; set; }
        public int OrderNumber { get; set; }
        public int FileId { get; set; }
        public string Instructions { get; set; }
        public int CourseId { get; set; }
        public VideoFile Video { get; set; }
    }
}