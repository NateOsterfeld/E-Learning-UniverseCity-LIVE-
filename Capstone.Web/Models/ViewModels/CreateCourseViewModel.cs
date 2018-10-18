
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Capstone.Web
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "The Course Name is required")]
        [MaxLength(50, ErrorMessage = "The course name is too long. Please limit the name to 50 characters or less.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Course Description is required")]
        [MaxLength(8000, ErrorMessage = "The course description is too long." +
                                       "Please limit the description to 140 characters or less.")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public Difficulty DifficultyLevel { get; set; }

        [Required(ErrorMessage = "The Course Cost is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Does that course contain the answer to the universe? Because no one's paying that much for the number 42")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Don't forget to add a picture for your course!")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PostedFile { get; set; }
    }

    public enum Difficulty
    {
        Beginner = 1,
        Easy = 2,
        Intermediate = 3,
        Difficult = 4,
        Expert = 5
    }
}