using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public class CourseEF : ICourse
    {
        private AppDbContext _context;

        public CourseEF(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            try
            {
                var deleteCourse = GetById(id);
                _context.Courses.Remove(deleteCourse);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Course> GetAll()
        {
            var course = _context.Courses.OrderBy(c => c.Title).ToList();
            return course;
        }

        public Course GetById(int id)
        {
            var courses = _context.Courses.FirstOrDefault(c => c.CourseID == id);
            return courses;
        }

        public IEnumerable<Course> GetByName(string text)
        {
            var courses = _context.Courses.Where(c => c.Title.Contains(text));
            return courses;
        }

        public Course Insert(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Course Update(Course course)
        {
            try
            {
                var courseUpdate = GetById(course.CourseID);
                courseUpdate.Title = course.Title;
                courseUpdate.Credits = course.Credits;
                _context.SaveChanges();
                return courseUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
