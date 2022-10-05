using TugasBootcampNET.Models;

namespace TugasBootcampNET.DTO
{
    public class EnrollmentAddDTO
    {
        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade? Grade { get; set; }
    }
}
