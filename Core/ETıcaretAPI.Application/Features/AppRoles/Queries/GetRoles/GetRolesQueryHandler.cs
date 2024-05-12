using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Role;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppRoles.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQueryRequest, GetRolesQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRolesQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async  Task<GetRolesQueryResponse> Handle(GetRolesQueryRequest request, CancellationToken cancellationToken)
        {
            GetRolesDto data = await _roleService.GetAllRoles(request.Page, request.Size);
            return new GetRolesQueryResponse()
            {
                Roles=data.Roles,
                TotalRoleCount=data.TotalRoleCount,
            };
        }
    }
}
