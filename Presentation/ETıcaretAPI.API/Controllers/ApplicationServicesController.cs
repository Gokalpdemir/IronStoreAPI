using ETıcaretAPI.Application.Abstractions.Services.Configurations;
using ETıcaretAPI.Application.CustomAttributes;
using ETıcaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETıcaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading,Definition ="Get Authorize Definition Endpoints",Menu ="Application Services")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
           var data =     _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(data);
        }
    }
}
