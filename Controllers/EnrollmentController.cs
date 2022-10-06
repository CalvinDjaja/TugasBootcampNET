using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TugasBootcampNET.DAL;
using TugasBootcampNET.DTO;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.Controllers
{
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

        [HttpGet("LastName")] //GetByName
        public IEnumerable<EnrollmentGetDTO> GetByName(string LastName)
        {
            var results = _enrollment.GetByName(LastName);
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

        [HttpPut] //Update
        public IActionResult Put(int id, EnrollmentAddDTO enrollmentAddDTO)
        {
            try
            {
                var enrollment = new Enrollment
                {
                    EnrollmentID = id,
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


    }
}
