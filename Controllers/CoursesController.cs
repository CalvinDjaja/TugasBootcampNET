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
    public class CoursesController : ControllerBase
    {
        private readonly ICourse _course;
        private readonly IMapper _mapper;

        public CoursesController(ICourse course, IMapper mapper)
        {
            _course = course;
            _mapper = mapper;
        }

        [HttpGet] //Read
        public IEnumerable<CourseGetDTO> Get()
        {
            var results = _course.GetAll();
            var lstCourseGetDto = _mapper.Map<IEnumerable<CourseGetDTO>>(results);
            return lstCourseGetDto;
        }

        [HttpGet("{id}")] //GetById
        public CourseGetDTO Get(int id)
        {
            var result = _course.GetById(id);
            var courseGetDto = _mapper.Map<CourseGetDTO>(result);
            return courseGetDto;
        }

        [HttpGet("{Title}")] //GetByName
        public IEnumerable<CourseGetDTO> GetByName(string name)
        {
            var results = _course.GetByName(name);
            var listCourseGetDTO = _mapper.Map<IEnumerable<CourseGetDTO>>(results);
            return listCourseGetDTO;
        }

        [HttpPost] //Create
        public IActionResult Post(CourseAddDTO courseDto)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDto);
                var newCourse = _course.Insert(course);
                var courseGetDTO = _mapper.Map<CourseGetDTO>(newCourse);
                return CreatedAtAction("Get", new { id = courseGetDTO.CourseID }, courseGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut] //Update
        public IActionResult Put(int id, CourseAddDTO courseDTO)
        {
            try
            {
                var course = new Course
                {
                    CourseID = id,
                    Title = courseDTO.Title
                };
                var editcourse = _course.Update(course);
                var courseGetDTO = _mapper.Map<CourseGetDTO>(editcourse);
                return Ok(courseGetDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")] //Delete
        public IActionResult Delete(int id)
        {
            try
            {
                _course.Delete(id);
                return Ok($"Delete course id {id} berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
