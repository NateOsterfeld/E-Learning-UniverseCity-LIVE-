
using Capstone.Web.Models.Data;

namespace Capstone.Web
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Description { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public int Difficulty { get; set; }
        public decimal CostUSD { get; set; }
        public double? CourseRating { get; set; }
        public int CourseFileId { get; set; }
        public ImageFile Image { get; set; }
    }
}