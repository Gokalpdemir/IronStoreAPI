using ETıcaretAPI.Application.Features.AppUsers.Commands.GoogleLogin;
using ETıcaretAPI.Application.Features.AppUsers.Commands.Login;
using ETıcaretAPI.Application.Features.AppUsers.Commands.PasswordReset;
using ETıcaretAPI.Application.Features.AppUsers.Commands.RefreshToken;
using ETıcaretAPI.Application.Features.AppUsers.Commands.VerifyResetToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody]RefreshTokenCommandRequest refreshTokenCommandRequest)
        {
            RefreshTokenCommandResponse refreshTokenCommandResponse = await _mediator.Send(refreshTokenCommandRequest);
            return Ok(refreshTokenCommandResponse);
        }
        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody]PasswordResetCommandRequest passwordResetCommandRequest)
        {
          PasswordResetCommandResponse response= await _mediator.Send(passwordResetCommandRequest);
            return Ok(response);
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken(VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
            return Ok(response);
        }
    }
}
