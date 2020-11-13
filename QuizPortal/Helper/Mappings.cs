using AutoMapper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;

namespace QuizPortal.Helper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
