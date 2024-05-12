using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppRoles.Commands.Update
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
    {
        private readonly IRoleService _roleService;
        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
           var result = await  _roleService.UpdateRoleAsync(request.Id, request.Name);
            return new UpdateRoleCommandResponse()
            {
                IsSuccess = result,
            };
        }
    }
}
