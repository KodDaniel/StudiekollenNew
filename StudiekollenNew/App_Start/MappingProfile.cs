using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using StudiekollenNew.Dtos;
using StudiekollenNew.Models;

namespace StudiekollenNew.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Test, TestDto>();
            Mapper.CreateMap<TestDto, Test>();
        }
    }
}