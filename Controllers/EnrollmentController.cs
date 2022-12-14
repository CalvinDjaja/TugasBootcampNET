using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TugasBootcampNET.DAL;
using TugasBootcampNET.DTO;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollment _enrollment;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollment enrollment, IMapper mapper)
        {
            _enrollment = enrollment;
            _mapper = mapper;
        }

        [HttpGet] //Read
        public IEnumerable<EnrollmentGetDTO> Get()
        {
            var results = _enrollment.GetAll();
            var lstenrollmentGetDto = _mapper.Map<IEnumerable<EnrollmentGetDTO>>(results);
            return lstenrollmentGetDto;
        }

        [HttpGet("{EnrollmentID}")] //GetById
        public EnrollmentGetDTO Get(int EnrollmentID)
        {
            var result = _enrollment.GetById(EnrollmentID);
            var enrollmentGetDto = _mapper.Map<EnrollmentGetDTO>(result);
            return enrollmentGetDto;
        }

        [HttpGet("Grade")] //GetByName
        public IEnumerable<EnrollmentGetDTO> GetByName(int Grade)
        {
            var results = _enrollment.GetByName(Grade);
            var listenrollmentGetDTO = _mapper.Map<IEnumerable<EnrollmentGetDTO>>(results);
            return listenrollmentGetDTO;
        }

        [HttpPost] //Create
        public IActionResult Post(EnrollmentAddDTO enrollmentAddDTO)
        {
            try
            {
                var enrollment = _mapper.Map<Enrollment>(enrollmentAddDTO);
                var newenrollment = _enrollment.Insert(enrollment);
                var enrollmentGetDTO = _mapper.Map<EnrollmentGetDTO>(newenrollment);
                return CreatedAtAction("Get", new { id = enrollmentGetDTO.EnrollmentID }, enrollmentGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{EnrollmentID}")] //Update
        public IActionResult Put(int EnrollmentID, EnrollmentAddDTO enrollmentAddDTO)
        {
            try
            {
                var enrollment = new Enrollment
                {
                    EnrollmentID = EnrollmentID,
                    CourseID = enrollmentAddDTO.CourseID,
                    StudentID = enrollmentAddDTO.StudentID
                };
                var editenrollment = _enrollment.Update(enrollment);
                var courseGetDTO = _mapper.Map<EnrollmentGetDTO>(editenrollment);
                return Ok(courseGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{EnrollmentID}")] //Delete
        public IActionResult Delete(int EnrollmentID)
        {
            try
            {
                _enrollment.Delete(EnrollmentID);
                return Ok($"Delete enrollment id {EnrollmentID} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertStudentToCourse")]
        public IActionResult AddStudentToCourse(AddStudentToCourseDTO addStudentToCourseDTO)
        {
            try
            {
                _enrollment.AddStudentToCourse(addStudentToCourseDTO.studentID, addStudentToCourseDTO.courseID);
                return Ok($"Student id {addStudentToCourseDTO.studentID} berhasil ditambahkan ke course {addStudentToCourseDTO.courseID}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteStudentFromCourse")]
        public IActionResult DeleteStudentFromCourse(int studentID, int courseID)
        {
            try
            {
                _enrollment.DeleteStudentFromCourse(studentID, courseID);
                return Ok($"Student id {studentID} berhasil dihilangkan dari course {courseID}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteAllStudentFromCourse/{courseID}")]
        public IActionResult DeleteAllStudentFromCourse(int courseID)
        {
            try
            {
                _enrollment.DeleteAllStudentFromCourse(courseID);
                return Ok($"Semua Student berhasil dihilangkan dari course {courseID}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
