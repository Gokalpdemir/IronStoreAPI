using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Role;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppRoles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _roleService.GetAllRolesNoPaginate();
            return new GetAllRolesQueryResponse()
            {
                Roles = data
            };
        }
    }
}
