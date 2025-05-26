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
            // User 
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<User, EditUserDto>();
            CreateMap<EditUserDto, User>();
            // LabTesting
            CreateMap<CreateLabTestDto, LabTest>();
            CreateMap<LabTest, LabTestDto>();
            CreateMap<LabTestDto, LabTest>();
            CreateMap<LabTestResult, LabTestResultDto>();
            CreateMap<LabTestResultDto, LabTestResult>();
            CreateMap<EditLabTestDto, LabTest>();
            CreateMap<EditLabTestResultDto, LabTestResult>();
            CreateMap<LabTest, EditLabTestDto>();
            CreateMap<LabTestResult, EditLabTestResultDto>();
            // Research
            CreateMap<CreateResearchDto, Research>();
            CreateMap<ResearchDto, Research>();
            CreateMap<Research, ResearchDto>();
            CreateMap<EditResearchDto, Research>()
                .ForMember(dest => dest.OwnerId, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.OwnerId));
                    opt.MapFrom(src => Guid.Parse(src.OwnerId!));
                });
            CreateMap<Research, EditResearchDto>()
                .ForMember(dest=>dest.OwnerId,opt=>opt.MapFrom(src=>src.OwnerId.ToString()));

        }
    }
}
