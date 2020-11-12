using AutoMapper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPortal.Helper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
        }
    }
}
