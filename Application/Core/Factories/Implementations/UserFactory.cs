using Application.Core.Factories.Interfaces;
using Application.DTO.General;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Factories.Implementations
{
    public class UserFactory : IUserFactory
    {
        private readonly UserManager<User> _userManager;

        public UserFactory(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> CreateUserAsync(RegisterDto registerDto, string? userRole = "Patient")
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidDataProvidedException("Email");
            }
            var user = new User
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                City = registerDto.City,
                Country = registerDto.Country,
                HouseNumber = registerDto.HouseNumber,
                EmailConfirmed = true,
            };
            user.UserName = registerDto.Email;
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRole);  
                return (await _userManager.FindByIdAsync(user.Id.ToString())) ?? throw new EntityNotFoundException("User");
            }
            else
            {
                throw new EntityCreatingException("User","UserFactory.CreateUser");
            }   
        }
    }
}
