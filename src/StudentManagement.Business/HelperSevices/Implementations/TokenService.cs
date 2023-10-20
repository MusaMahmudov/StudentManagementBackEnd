using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.HelperSevices.Interfaces;
using StudentManagement.Core.Entities.Identity;
using StudentManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly IConfiguration _configuration;
        public TokenService(UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<TokenResponseDTO> CreateToken(AppUser User)
        {

            List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,User.UserName),
            new Claim(ClaimTypes.Email,User.Email),
            new Claim(ClaimTypes.NameIdentifier,User.Id),
        };
            foreach (var role in await _userManager.GetRolesAsync(User))
            {
                claims.Add(new Claim(Enum.GetValues(typeof(Roles)).ToString(), role));
            }
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));


            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(4)
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TokenResponseDTO(token, jwt.ValidTo);
        }
    }
}
