using DemoBookApp.Application.Decorators;
using DemoBookApp.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/authors"), ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorPersistenceDecorator _persistence;
        public AuthorController(AuthorPersistenceDecorator persistence)
        {
            _persistence = persistence;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] AuthorQuery query, CancellationToken token)
        {
            var result = await _persistence.GetAsync(query, token);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
    }
}