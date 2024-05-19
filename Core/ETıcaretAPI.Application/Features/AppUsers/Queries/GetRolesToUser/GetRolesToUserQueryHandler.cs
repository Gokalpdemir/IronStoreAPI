using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppUsers.Queries.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        private readonly IUserService _userService;

        public GetRolesToUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
           var result = await _userService.GetRolesToUserAsync(request.Id);
            return new GetRolesToUserQueryResponse()
            {
                UserRoles = result
            };
        }
    }
}
