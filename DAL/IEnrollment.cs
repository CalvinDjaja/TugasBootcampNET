using TugasBootcampNET.Models;

namespace TugasBootcampNET.DAL
{
    public interface IEnrollment
    {
        public Enrollment Insert(Enrollment enrollment); //Create
        public IEnumerable<Enrollment> GetAll(); //Read
        public Enrollment Update(Enrollment enrollment); //Update
        public void Delete(int id);//Delete
        public IEnumerable<Enrollment> GetByName(string text); //getbyname
        public Enrollment GetById(int id); //getbyid
    }
}
