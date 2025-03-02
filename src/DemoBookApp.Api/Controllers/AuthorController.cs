using DemoBookApp.Application.Decorators;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/authors"), ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _persistence;
        public AuthorController(IAuthorRepository persistence)
        {
            _persistence = persistence;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] AuthorQuery query, CancellationToken token)
        {
            var result = await _persistence.GetAsync(query, token);

            if (result is null)
                return NotFound();

            return Ok(result.Select(a => a.ToGetResponse()));
        }
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id, CancellationToken token)
        {
            var result = await _persistence.GetByIdAsync(id, token);

            if (result is null)
                return NotFound();

            return Ok(result.ToGetResponse());
        }
    }
}