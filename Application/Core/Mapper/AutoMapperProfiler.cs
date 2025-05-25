using Application.DTO.LabTesting;
using Application.DTO.Researches;
using Application.DTO.Users;
using AutoMapper;
using Domain.Models.LabTesting;
using Domain.Models.Researches;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Mapper
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<CreateLabTestDto, LabTest>();
            CreateMap<LabTest, LabTestDto>();
            CreateMap<LabTestDto, LabTest>();
            CreateMap<CreateResearchDto, Research>();
            CreateMap<ResearchDto, Research>();
            CreateMap<Research, ResearchDto>();
            CreateMap<LabTestResult, LabTestResultDto>();
            CreateMap<LabTestResultDto, LabTestResult>();
        }
    }
}
