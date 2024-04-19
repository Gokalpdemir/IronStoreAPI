using ETıcaretAPI.Application.Abstractions.Services.Authentication;
using ETıcaretAPI.Application.Dtos;
using MediatR;


namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IInternalAuthentication _ınternalAuthentication;
public LoginUserCommandHandler(IInternalAuthentication ınternalAuthentication)
        {
            _ınternalAuthentication = ınternalAuthentication;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

           Token token= await _ınternalAuthentication.LoginAsync(request.UserNameOrEmail, request.Password,5);
            return new LoginUserCommandResponse()
            {
                Token = token,
            };
        }
    }
}
