using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TugasBootcampNET.DAL;
using TugasBootcampNET.DTO;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudent _student;
        private readonly IMapper _mapper;

        public StudentsController(IStudent student, IMapper mapper)
        {
            _student = student;
            _mapper = mapper;
        }

        [HttpGet] //Read
        public IEnumerable<StudentGetDTO> Get()
        {
            var results = _student.GetAll();
            var lstStudentGetDto = _mapper.Map<IEnumerable<StudentGetDTO>>(results);
            return lstStudentGetDto;
        }

        [HttpGet("{ID}")] //GetById
        public StudentGetDTO Get(int ID)
        {
            var result = _student.GetById(ID);
            var studentGetDto = _mapper.Map<StudentGetDTO>(result);
            return studentGetDto;
        }

        [HttpGet("LastName")] //GetByName
        public IEnumerable<StudentGetDTO> GetByName(string LastName)
        {
            var results = _student.GetByName(LastName);
            var listStudentGetDTO = _mapper.Map<IEnumerable<StudentGetDTO>>(results);
            return listStudentGetDTO;
        }

        [HttpPost] //Create
        public IActionResult Post(StudentAddDTO studentAddDTO)
        {
            try
            {
                var student = _mapper.Map<Student>(studentAddDTO);
                var newStudent = _student.Insert(student);
                var studentGetDTO = _mapper.Map<StudentGetDTO>(newStudent);
                return CreatedAtAction("Get", new { id = studentGetDTO.ID }, studentGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] //Update
        public IActionResult Put(int id, StudentAddDTO studentAddDTO)
        {
            try
            {
                var student = new Student
                {
                    ID = id,
                    LastName = studentAddDTO.LastName,
                    FirstMidName = studentAddDTO.FirstMidName,
                    EnrollmentDate = studentAddDTO.EnrollmentDate
                };
                var editStudent = _student.Update(student);
                var courseGetDTO = _mapper.Map<StudentGetDTO>(editStudent);
                return Ok(courseGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ID}")] //Delete
        public IActionResult Delete(int ID)
        {
            try
            {
                _student.Delete(ID);
                return Ok($"Delete student id {ID} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListStudent/{CourseID}")] //Studen With Chosen Course
        public IEnumerable<StudentGetDTO> AllStudentWithChosenCourse(int CourseID)
        {
            var results = _student.AllStudentWithChosenCourse(CourseID);
            var listStudentGetDTO = _mapper.Map<IEnumerable<StudentGetDTO>>(results);
            return listStudentGetDTO;
        }

        [HttpGet("ListStudent")] //Studen With Course
        public IEnumerable<StudentWithCourseGetDTO> AllStudentWithCourse()
        {
            var results = _student.AllStudentWithCourse();
            var listStudentGetDTO = _mapper.Map<IEnumerable<StudentWithCourseGetDTO>>(results);
            return listStudentGetDTO;
        }
    }
}
