namespace TugasBootcampNET.DTO
{
    public class StudentWithCourseGetDTO
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<CourseGetDTO> courses { get; set; }
    }
}
