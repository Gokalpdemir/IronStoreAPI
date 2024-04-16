using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Application.Dtos;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }
        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            }
            if (user == null)
            {
                throw new NotFoundUserException();
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded) //authentication başarılı Authorize işlemi yap.
            {
              Token token=  _tokenHandler.CreateAccessToken(5);
                return new LoginUserCommandResponse()
                {
                    Token = token,
                };
            }
           
            throw new AuthenticationErrorException();
            
        }
    }
}
