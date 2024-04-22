using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            RefreshTokenCommandResponse response = new();
            response.Token= await _authService.RefreshTokenLoginAsync(request.RefreshToken); 
            return response;
        }
    }
}
