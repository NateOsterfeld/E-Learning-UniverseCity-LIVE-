
namespace Capstone.Web
{
    public class StudentAssignment
    {
        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public int FileId { get; set; }
        public float Grade { get; set; }
        public string TeacherComments { get; set; }
        public string AssignmentName { get; set; }
    }
}