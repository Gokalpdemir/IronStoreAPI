using AutoMapper;
using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Dtos.User;
using ETıcaretAPI.Application.Exceptions;
using ETıcaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETıcaretAPI.Application.Features.AppUsers.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreatedUserCommandResponse>
    {
        
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public CreateUserCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<CreatedUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

         CreatedUserDto response=   await _userService.CreateAsync(_mapper.Map<CreateUserDto>(request));
            return _mapper.Map<CreatedUserCommandResponse>(response);

        }
    }
}
