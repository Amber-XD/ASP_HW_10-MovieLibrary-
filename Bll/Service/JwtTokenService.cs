
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bll.DTO;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bll.Service
{
    public class JwtTokenService(IConfiguration configuration)
    {
        public LoginResponseDto CreateToken(User user)
        {
            var expires = DateTime.UtcNow.AddMinutes(
                int.Parse(configuration["JWT:expiresMinutes"]!)
            );

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.UniqueName, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["JWT:key"]!)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:issuer"],
                audience: configuration["JWT:audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new LoginResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                ExpiresAtUtc = expires
            };
        }
    }
}
