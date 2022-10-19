using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public interface IStudent
    {
        public Student Insert(Student student); //Create
        public IEnumerable<Student> GetAll(); //Read
        public Student Update(Student student); //Update
        public void Delete(int id);//Delete
        public IEnumerable<Student> GetByName(string text); //getbyname
        public Student GetById(int id); //getbyid
        public IEnumerable<Student> AllStudentWithChosenCourse(int CourseID);
        public IEnumerable<Student> AllStudentWithCourse();
        public Student InsertWithCourse(Student student, int course); //Create
        public IEnumerable<Student> GetAllWithPage(PageParameters pageParameters); //Read

    }
}
