using ETıcaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETıcaretAPI.Application.Features.AppUsers.Queries.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQueryRequest, GetAllUserQueryResponse>
    {
        private readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUserQueryResponse> Handle(GetAllUserQueryRequest request, CancellationToken cancellationToken)
        {
          var result= await _userService.GetAllUsersAsync(request.Page,request.Size);
            return new GetAllUserQueryResponse
            {
                Users = result,
                TotalUserCount = _userService.TotalUsersCount,
            };
        }
    }
}
