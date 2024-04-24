using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Abstractions.Services.Authentication;
using ETıcaretAPI.Application.Dtos;
using MediatR;


namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IInternalAuthentication _ınternalAuthentication;
        readonly private IUserService _userService;
        public LoginUserCommandHandler(IInternalAuthentication ınternalAuthentication, IUserService userService)
        {
            _ınternalAuthentication = ınternalAuthentication;
            _userService = userService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

           Token token= await _ınternalAuthentication.LoginAsync(request.UserNameOrEmail, request.Password,900);      
            return new LoginUserCommandResponse()
            {
                Token = token,
            };
        }
    }
}
