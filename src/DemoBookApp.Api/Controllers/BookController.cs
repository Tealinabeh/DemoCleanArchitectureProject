using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/books"), ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _persistence;
        public BookController(IBookRepository persistence)
        {
            _persistence = persistence;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] BookQuery query, CancellationToken token)
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