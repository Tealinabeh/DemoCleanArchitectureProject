using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/books"), ApiController]
    public class BookController : ControllerBase
    {
        private const string StatusCode500Message = "An unexpected error occurred.";

        private readonly IBookHandler _persistence;
        public BookController(IBookHandler persistence)
        {
            _persistence = persistence;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] BookQuery query, CancellationToken token)
        {
            var result = await _persistence.GetAsync(query, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    QueryArgumentException => BadRequest(result.Exception.Message),
                    ArgumentException => NotFound(result.Exception.Message),
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, StatusCode500Message)
                };
            }
            return Ok(result.Value.Select(b => b.ToGetResponse()));
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id, CancellationToken token)
        {
            var result = await _persistence.GetByIdAsync(id, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, StatusCode500Message)
                };
            }
            return Ok(result.Value.ToGetResponse());
        }
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, UpdateBookRequest request, CancellationToken token)
        {
            var result = await _persistence.UpdateAsync(id, request, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, StatusCode500Message)
                };
            }
            return NoContent();
        }
        [HttpPost]
            public async Task<IActionResult> CreateAsync([FromBody]CreateBookRequest request, CancellationToken token)
        {
            var result = await _persistence.CreateAsync(request, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, StatusCode500Message)
                };
            }
            return NoContent();
        }
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]long id, CancellationToken token)
        {
            var result = await _persistence.DeleteAsync(id, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, StatusCode500Message)
                };
            }
            return NoContent();
        }
    }   
}