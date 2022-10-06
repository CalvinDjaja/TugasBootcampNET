using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public interface ICourse
    {
        public Course Insert(Course course); //Create
        public IEnumerable<Course> GetAll(); //Read
        public Course Update(Course course); //Update
        public void Delete(int id);//Delete
        public IEnumerable<Course> GetByName(string text); //getbyname
        public Course GetById(int id); //getbyid
        public IEnumerable<Course> AllCourseWithChosenStudent(int StudentID);
    }   
}
