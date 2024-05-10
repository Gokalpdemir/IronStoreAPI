using Azure.Core;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Application.Dtos;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Application.Helpers;
using ETıcaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AuthService(ITokenHandler tokenHandler, UserManager<AppUser> userManager, IConfiguration configuration, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
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
                throw new Exception("Hata");
            }
            Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);
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

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded) //authentication başarılı Authorize işlemi yap.
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 15);

                return token;
            }

            throw new AuthenticationErrorException("Kullanıcı adı veya Şifresi hatalı");
            
        }


        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                resetToken = resetToken.UrlEncode();
                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {

                resetToken = resetToken.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}
