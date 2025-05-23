using Application.DTO.Users;
using Application.Services.Interfaces.Users;
using AutoMapper;
using Domain.Interfaces.Commands.UsersCommands;
using Domain.Interfaces.Queries.UserQueries;
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
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public UserService(IUserCommandRepository userCommandRepository, IUserQueryRepository userQueryRepository,IMapper mapper)
        {
            _userCommandRepository = userCommandRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
        }

        public Task DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserDto>> GetAllUsersPaginatedAsync(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            return _mapper.Map<UserDto>(await _userQueryRepository.GetUserByEmailAsync(email));
        }

        public Task<UserDto> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<UserDto>> GetUsersByResearchIdAsync(Guid researchId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
