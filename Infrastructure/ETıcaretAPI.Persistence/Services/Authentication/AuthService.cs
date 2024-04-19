using Azure.Core;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Application.Dtos;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Persistence.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(ITokenHandler tokenHandler, UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager)
        {
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }


        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_Id"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
            UserLoginInfo userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE"); // AspNetUserLogins tablosuna kayıt edilcek UserLoginInfo nesnesi oluşturuldu
            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                result = true;
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name,
                    };
                    IdentityResult identityResult = await _userManager.CreateAsync(user); //aspNetUsers kayıt
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, userLoginInfo);
            }
            else
            {
                throw new Exception("hata aldıkkkkkk");
            }
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
            return token;
        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userNameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,password, false);

            if (result.Succeeded) //authentication başarılı Authorize işlemi yap.
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime);
                return token;
            }

            throw new AuthenticationErrorException();
        }
    }
}
