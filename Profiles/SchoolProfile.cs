using AutoMapper;
using TugasBootcampNET.DTO;
using TugasBootcampNET.Models;

namespace TugasBootcampNET.Profiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<CourseAddDTO, Course>();
            CreateMap<Course, CourseGetDTO>();
            CreateMap<StudentAddDTO, Student>();
            CreateMap<Student, StudentGetDTO>();
        }

    }
}
