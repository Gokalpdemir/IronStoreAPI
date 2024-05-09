using MediatR;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.PasswordReset
{
    public class PasswordResetCommandRequest:IRequest<PasswordResetCommandResponse> 
    {
        public string Email { get; set; }
    }
}
