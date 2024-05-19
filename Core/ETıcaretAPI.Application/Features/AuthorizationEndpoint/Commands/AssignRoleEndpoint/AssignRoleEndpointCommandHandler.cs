using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ETıcaretAPI.Application.Features.AuthorizationEndpoint.Commands.AssignRoleEndpoint
{
    public class AssignRoleEndpointCommandHandler : IRequestHandler<AssignRoleEndpointCommandRequest, AssignRoleEndpointCommandResponse>
    {
        private readonly IAuthorizationEndpointService _authorizationEndpointService;

        public AssignRoleEndpointCommandHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<AssignRoleEndpointCommandResponse> Handle(AssignRoleEndpointCommandRequest request, CancellationToken cancellationToken)
        {
          await  _authorizationEndpointService.AssignRoleEndpoint(request.Roles, request.Menu, request.Code, request.Type);
            return new AssignRoleEndpointCommandResponse();
        }
    }
}
