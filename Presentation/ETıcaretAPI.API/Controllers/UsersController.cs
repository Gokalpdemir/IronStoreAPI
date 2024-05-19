using ETıcaretAPI.Application.Abstractions.Services;
using ETıcaretAPI.Application.Const;
using ETıcaretAPI.Application.CustomAttributes;
using ETıcaretAPI.Application.Enums;
using ETıcaretAPI.Application.Features.AppUsers.Commands.AssignRoleToUser;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Create;
using ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Application.Features.AppUsers.Commands.UpdatePassword;
using ETıcaretAPI.Application.Features.AppUsers.Queries.GetAll;
using ETıcaretAPI.Application.Features.AppUsers.Queries.GetRolesToUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = ActionType.Reading, Definition = "Get All Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUserQueryRequest getAllUserQueryRequest)
        {
            GetAllUserQueryResponse reponse = await _mediator.Send(getAllUserQueryRequest);
            return Ok(reponse);
        }

        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = ActionType.Reading, Definition = "Get Roles To User")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
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

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users, ActionType = ActionType.Writing, Definition = "Assign Role To User")]

        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
           AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }

       
    }
}
