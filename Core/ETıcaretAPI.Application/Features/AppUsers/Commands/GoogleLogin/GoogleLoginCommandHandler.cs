using ETıcaretAPI.Application.Abstractions.Token;
using ETıcaretAPI.Application.Dtos;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }



        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "244401510303-1f6p706dmeojlkmq0uci3peq91vd575t.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            UserLoginInfo userLoginInfo = new UserLoginInfo(request.provider, payload.Subject, request.provider); // AspNetUserLogins tablosuna kayıt edilcek UserLoginInfo nesnesi oluşturuldu
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


            Token token = _tokenHandler.CreateAccessToken(5);
            GoogleLoginCommandResponse response = new();
            response.Token = token;
            return response;

        }
    }

}
