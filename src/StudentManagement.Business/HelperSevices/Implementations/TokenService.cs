using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Business.DTOs.AuthDTOs;
using StudentManagement.Business.DTOs.StudentDTOs;
using StudentManagement.Business.DTOs.TeacherDTOs;
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
        public async Task<TokenResponseDTO> CreateToken(AppUser User,StudentForTokenDTO? student,TeacherForTokenDTO? teacher,bool rememberMe)
        {
            //DateTime expireTime = DateTime.UtcNow;
            //if (rememberMe)
            //{
            //    expireTime.AddMonths(3);
            //}
            //else
            //{
            //    expireTime.AddHours(6);
            //}

            var roles =await _userManager.GetRolesAsync(User);

            List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,User.UserName),
            new Claim(ClaimTypes.Email,User.Email),
            new Claim(ClaimTypes.NameIdentifier,User.Id),
        };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            if(student is not null)
            {
                claims.Add(new Claim("FullName",student.FullName));
                claims.Add(new Claim("Id", student.Id.ToString()));

            }
            if (teacher is not null)
            {
                claims.Add(new Claim("FullName", teacher.FullName));
                claims.Add(new Claim("Id", teacher.Id.ToString()));


            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));


            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                notBefore: DateTime.UtcNow,
                expires: rememberMe ? DateTime.UtcNow.AddMonths(3) : DateTime.UtcNow.AddHours(6)
                );
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TokenResponseDTO(token, jwt.ValidTo);
        }
    }
}
