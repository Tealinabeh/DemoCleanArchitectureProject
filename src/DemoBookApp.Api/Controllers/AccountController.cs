using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Contracts.Responses;
using DemoBookApp.Core;
using Microsoft.AspNetCore.Identity;
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
    }
}