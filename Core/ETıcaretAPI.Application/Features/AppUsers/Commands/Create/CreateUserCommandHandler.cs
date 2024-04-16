using AutoMapper;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<CreatedUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = _mapper.Map<AppUser>(request);
            user.Id=Guid.NewGuid().ToString();
            
          IdentityResult result=   await _userManager.CreateAsync(user,request.Password);
            CreatedUserCommandResponse response= new CreatedUserCommandResponse() { Succeeded=result.Succeeded};
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla kaydedilmiştir";
            else
            {
                foreach (var err in result.Errors)
                {
                    response.Message += $"{err.Code} - {err.Description}\n";
                }
            }

            return response;

        }
    }
}
