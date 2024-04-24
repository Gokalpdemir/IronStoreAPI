using ETıcaretAPI.Application.Abstractions.Services.Authentication;
using ETıcaretAPI.Application.Dtos;

using MediatR;


namespace ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
       private readonly IExternalAuthentication _externalAuthentication;

        public GoogleLoginCommandHandler(IExternalAuthentication externalAuthentication)
        {
            _externalAuthentication = externalAuthentication;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

          Token token=   await _externalAuthentication.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Token = token
            };

        }
    }

}
