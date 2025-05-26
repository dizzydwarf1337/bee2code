using Application.Services.Interfaces.General;
using Domain.Exceptions.BusinessExceptions;
using Domain.Exceptions.DataExceptions;
using Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations.General
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<User> _userManager;

        private readonly byte[] _key;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _jwtSettings = _configuration.GetSection("JwtSettings");

            _key = Encoding.UTF8.GetBytes(_jwtSettings["Key"] ?? throw new ConfigException("JWT Key is missing"));
            _issuer = _jwtSettings["Issuer"] ?? throw new ConfigException("JWT Issuer is missing");
            _audience = _jwtSettings["Audience"] ?? throw new ConfigException("JWT Audience is missing");
        }

        public async Task<bool> CheckUserToken(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new EntityNotFoundException("User");
            var dbToken = await _userManager.GetAuthenticationTokenAsync(user, "Default", "Jwt bearer") ?? throw new EntityNotFoundException("Token");
            if (dbToken != null && dbToken == token)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteUserToken(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new EntityNotFoundException("User");
            _ = await _userManager.GetAuthenticationTokenAsync(user, "Default", "Jwt bearer") ?? throw new EntityNotFoundException("Token");
            await _userManager.RemoveAuthenticationTokenAsync(user, "Default", "Jwt bearer");
        }

        public async Task<string> GetLoginToken(Guid userId)
        {
            string token;
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new EntityNotFoundException("User");
            try
            {
                token = await _userManager.GetAuthenticationTokenAsync(user,"Default","Jwt bearer") ?? throw new Exception();
            }
            catch (Exception)
            {
                token = await GenerateAuthToken(userId);
            }
            return token;
        }
        private string CreateJwtToken(Guid userId, IEnumerable<Claim> claims)
        {
            var symmetricKey = new SymmetricSecurityKey(_key);
            var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(4),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<string> GenerateAuthToken(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new InvalidDataProvidedException("Wrong user Id");
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = CreateJwtToken(userId, claims);
            await _userManager.SetAuthenticationTokenAsync(user, "Default", "Jwt bearer", token);
            return token;
        }
    }
}
