using Application.DTO.Researches;
using Application.DTO.Users;
using Application.Services.Interfaces.Users;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.ResearchesQueries;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.Users
{
    public class UserService : IUserService
    {
        private readonly IUserCommandRepository _userCommandRepository;
        private readonly IResearchQueryRepository _researchQueryRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public UserService(IUserCommandRepository userCommandRepository, 
            IUserQueryRepository userQueryRepository,
            IMapper mapper,
            UserManager<User> userManager,
            IResearchQueryRepository researchQueryRepository
            )
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            _userManager = userManager;
            _researchQueryRepository = researchQueryRepository;
        }

        public async Task DeleteUser(Guid userId)
        {
            await _userCommandRepository.DeleteUserAsync(userId);
        }
        public async Task<UserDto> UpdateUser(EditUserDto userDto)
        {
            var userUpdate = await _userQueryRepository.GetUserByIdAsync(Guid.Parse(userDto.Id));
            _mapper.Map(userDto, userUpdate);
            return _mapper.Map<UserDto>(await _userCommandRepository.UpdateUserAsync(userUpdate));
        }


        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userQueryRepository.GetUserByEmailAsync(email));
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            var user = await _userQueryRepository.GetUserByIdAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.MyResearch = _mapper.Map<List<ResearchPreviewDto>>(await _researchQueryRepository.GetResearchesByOwnerIdPaginatedAsync(userId,1,int.MaxValue));
            userDto.PatientResearches = _mapper.Map<List<ResearchPreviewDto>>(await _researchQueryRepository.GetResearchesByPatientIdPaginatedAsync(userId,1,int.MaxValue));
            return userDto;
        }



        public async Task GrantUserRole(Guid userId, string role)
        {
            var user = await _userQueryRepository.GetUserByIdAsync(userId);
            Console.WriteLine(role);
            if (!String.Equals(role,"Admin") && !String.Equals(role,"Worker") && !String.Equals(role,"Patient")) throw new EntityNotFoundException("User role");
            await _userManager.RemoveFromRoleAsync(user,"Admin");
            await _userManager.RemoveFromRoleAsync(user, "Worker");
            await _userManager.RemoveFromRoleAsync(user, "Patient");
            await _userManager.AddToRoleAsync(user, role);
            await _userManager.RemoveAuthenticationTokenAsync(user, "Default", "Jwt bearer");
        }

        public async Task<List<UserDto>> GetAllUsersPaginatedAsync(int page, int pageSize)
        {
            return _mapper.Map<List<UserDto>>(await _userQueryRepository.GetAllUsersPaginatedAsync(page, pageSize));
        }
    }
}
