using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.Dtos.Token CreateAccessToken(int second, AppUser user)
        {
            Application.Dtos.Token token = new();

            // securitykey symetriği alınır
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //şifreleme kimliği oluşturuldu
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // oluşturulacak token ayarları
            token.Expiration = DateTime.UtcNow.AddSeconds(second );
            JwtSecurityToken SecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims:new List<Claim> { new (ClaimTypes.Name,user.UserName)} 

                );
            // token oluşturucu sınıfından bir örnek aldık.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // örneğe tokenımızı yazdık.
            token.AccessToken = tokenHandler.WriteToken(SecurityToken);

            token.RefreshToken=  CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);

        }
    }
}
