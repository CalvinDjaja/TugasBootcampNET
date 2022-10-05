using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public class EnrollmentEF : IEnrollment
    {
        private AppDbContext _context;

        public EnrollmentEF(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            try
            {
                var enrollments = GetById(id);
                _context.Enrollments.Remove(enrollments);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Enrollment> GetAll()
        {
            var enrollments = _context.Enrollments.OrderBy(s => s.Grade).ToList();
            return enrollments;
        }

        public Enrollment GetById(int id)
        {
            var enrollments = _context.Enrollments.FirstOrDefault(s => s.EnrollmentID == id);
            return enrollments;
        }

        public IEnumerable<Enrollment> GetByName(string text)
        {
            var enrollments = _context.Enrollments.Where(s => s.Grade.ToString().Contains(text)).ToList();
            return enrollments;
        }

        public Enrollment Insert(Enrollment enrollment)
        {
            try
            {
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return enrollment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Enrollment Update(Enrollment enrollment)
        {
            try
            {
                var enrollments = GetById(enrollment.EnrollmentID);
                enrollments.Grade = enrollment.Grade;
                enrollments.CourseID = enrollment.CourseID;
                enrollments.StudentID = enrollment.StudentID;
                _context.SaveChanges();
                return enrollments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }
    }
}
