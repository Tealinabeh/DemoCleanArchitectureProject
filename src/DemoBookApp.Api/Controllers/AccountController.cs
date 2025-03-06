using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts.Exceptions;
using DemoBookApp.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHandler _handler;

        public AccountController(IAccountHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("register")]
        [EndpointSummary("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserRequest request)
        {
            var result = await _handler.RegisterAsync(request);

            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    InvalidOperationException => BadRequest(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message),
                };
            }
            return StatusCode(201, result.Value);
        }
        [HttpPost("login")]
        [EndpointSummary("Log in")]
        public async Task<IActionResult> LogInAsync([FromBody] LogInUserRequest request)
        {
            var result = await _handler.LogInAsync(request);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    InvalidDataException => BadRequest(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message),
                };
            }
            return Ok(result.Value);
        }
        [HttpPost("changeRole")]
        [Authorize(Policy = "RequireAdminRole")]
        [EndpointSummary("Change role")]
        public async Task<IActionResult> ChangeRoleAsync([FromBody] ChangeRoleRequest request)
        {
            var result = await _handler.ChangeRoleAsync(request);

            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    InvalidOperationException => BadRequest(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message),
                };
            }
            return Ok();
        }
    }
}