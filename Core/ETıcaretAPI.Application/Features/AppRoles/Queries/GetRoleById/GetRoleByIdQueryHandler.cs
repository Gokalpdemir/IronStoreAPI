using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.Role;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppRoles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
    {
        private readonly IRoleService _roleService;

        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var getRoleDto = await _roleService.GetRolesByIdAsync(request.Id);
            return new GetRoleByIdQueryResponse()
            {
                Id = getRoleDto.Id,
                Name = getRoleDto.Name,

            };
        }
    }
}
