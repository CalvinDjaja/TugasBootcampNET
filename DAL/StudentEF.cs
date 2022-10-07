using Microsoft.EntityFrameworkCore;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public class StudentEF : IStudent
    {
        private AppDbContext _context;

        public StudentEF(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> AllStudentWithChosenCourse(int CourseID)
        {
            var students = _context.Students
                .FromSqlInterpolated($"exec dbo.AllStudentWithChosenCourse {CourseID}").ToList();
            return students;
        }

        public IEnumerable<Student> AllStudentWithCourse()
        {
            var students = _context.Students
                .FromSqlInterpolated($"exec dbo.AllStudentWithCourse").ToList();
            return students;
        }

        public void Delete(int id)
        {
            try
            {
                var students = GetById(id);
                _context.Students.Remove(students);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            var students = _context.Students.OrderBy(s => s.LastName).ToList();
            return students;
        }

        public Student GetById(int id)
        {
            var students = _context.Students.FirstOrDefault(s => s.ID == id);
            return students;
        }

        public IEnumerable<Student> GetByName(string text)
        {
            var students = _context.Students.Where(s => s.LastName.Contains(text)).ToList();
            return students;
        }

        public Student Insert(Student student)
        {
            try
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Student InsertWithCourse(Student student, int courseID)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseID == courseID);
            if (course == null)
            {
                throw new Exception($"Data course id {courseID} tidak ditemukan");
            }
            try
            {
                Enrollment enrollment = new Enrollment();
                enrollment.CourseID = courseID;
                enrollment.StudentID = student.ID;
                _context.Students.Add(student);
                _context.SaveChanges();
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return student;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Student Update(Student student)
        {
            try
            {
                var students = GetById(student.ID);
                students.FirstMidName = student.FirstMidName;   
                students.LastName = student.LastName;
                students.EnrollmentDate = student.EnrollmentDate;
                _context.SaveChanges();
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            };
        }
    }
}
