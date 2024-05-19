using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.AuthorizationEndpoint.Queries.GetRolesToEndpoints
{
    public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
    {
        private readonly IAuthorizationEndpointService _authorizationEndpointService;

        public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
        {
            _authorizationEndpointService = authorizationEndpointService;
        }

        public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code,request.Menu);
            return new GetRolesToEndpointQueryResponse
            {
                Roles = data,
            };
        }
    }
}
