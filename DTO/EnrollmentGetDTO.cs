using TugasBootcampNET.Models;

namespace TugasBootcampNET.DTO
{
    public class EnrollmentGetDTO
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade? Grade { get; set; }
    }
}
