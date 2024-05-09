using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Exceptions;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private  readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (!request.Password.Equals(request.PasswordConfirm))
                throw new PasswordChangeFaildException("Şifreler Eşleşmiyor");
                 await _userService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
            return new UpdatePasswordCommandResponse();
            
        }
    }
}
