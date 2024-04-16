using ETıcaretAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        public Application.Dtos.Token CreateAccessToken(int minute)
        {
            Application.Dtos.Token token = new();

            // securitykey symetriği alınır
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            //şifreleme kimliği oluşturuldu
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // oluşturulacak token ayarları
            token.Expiration = DateTime.Now.AddMinutes(minute);
            JwtSecurityToken SecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );
            // token oluşturucu sınıfından bir örnek aldık.
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            // örneğe tokenımızı yazdık.
            token.AccessToken = tokenHandler.WriteToken(SecurityToken);
            return token;

        }
    }
}
