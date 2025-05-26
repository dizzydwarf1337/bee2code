using Application.Core.Factories.Interfaces;
using Application.DTO.General;
using Application.DTO.Users;
using Application.Services.Interfaces.General;
using Application.Services.Interfaces.Users;
using AutoMapper;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using Domain.Interfaces.Queries.UserQueries;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.General
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserFactory _userFactory;
        private readonly ITokenService _tokenService;
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IMapper _mapper;

        public AuthService(UserManager<User> userManager, IUserFactory userFactory, ITokenService tokenService, IMapper mapper, IUserQueryRepository userQueryRepository)
        {
            _userManager = userManager;
            _userFactory = userFactory;
            _tokenService = tokenService;
            _mapper = mapper;
            _userQueryRepository = userQueryRepository;
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _userQueryRepository.GetUserByEmailAsync(loginDto.Email) ?? throw new EntityNotFoundException("User");
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect)
            {
                throw new InvalidDataProvidedException("Password", "User", "AuthService.Login");
            }
            var token = await _tokenService.GetLoginToken(user.Id);
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;
            userDto.Role = await _userManager.IsInRoleAsync(user, "Admin") ? "Admin" : (await _userManager.IsInRoleAsync(user, "Worker") ? "Worker" : "Patient");
            return userDto;
        }

        public async Task LogOut(string userMail)
        {
            var user = await _userQueryRepository.GetUserByEmailAsync(userMail) ?? throw new EntityNotFoundException("User");
            await _userManager.RemoveAuthenticationTokenAsync(user, "Default", "Jwt bearer"); 
        }

        public async Task Register(RegisterDto registerDto)
        {
            await _userFactory.CreateUserAsync(registerDto);
        }
    }
}
