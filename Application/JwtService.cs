using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using Domain.Abstations.Application;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Application
{
    public class JwtService : IJwtService
    {
        public string GenerateToken(string user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                 new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
            };

            var tokenDescriptor = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my test key")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
    }
}
