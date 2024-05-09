using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Create;
using ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Application.Features.AppUsers.Commands.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController()]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMailService _mailService;
        public UsersController(IMediator mediator, IMailService mailService)
        {
            _mediator = mediator;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> addUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreatedUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);

        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }
    }
}
