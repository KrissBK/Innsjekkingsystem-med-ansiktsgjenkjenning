using AutoMapper;
using FaceRecogApp.Dto;
using FaceRecogApp.Models;

namespace FaceRecogApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Picture, PictureDto>().ReverseMap();
        }
    }
}
