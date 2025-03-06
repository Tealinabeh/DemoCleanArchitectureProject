using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Exceptions;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/authors"), ApiController]
    [Authorize(Policy = "RequireAdminRole")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorHandler _persistence;
        public AuthorController(IAuthorHandler persistence)
        {
            _persistence = persistence;
        }
        
        [HttpGet, AllowAnonymous]
        [EndpointSummary("Get by query")]
        public async Task<IActionResult> GetAsync([FromQuery] AuthorQuery query, CancellationToken token)
        {
            var result = await _persistence.GetAsync(query, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    QueryArgumentException => BadRequest(result.Exception.Message),
                    ArgumentException => NotFound(result.Exception.Message),
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return Ok(result.Value.Select(a => a.ToGetResponse()));
        }

        [HttpGet("{id:long}"), AllowAnonymous]
        [EndpointSummary("Get by id")]
        public async Task<IActionResult> GetAsync([FromRoute] long id, CancellationToken token)
        {
            var result = await _persistence.GetByIdAsync(id, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return Ok(result.Value.ToGetResponse());
        }
        [HttpPut("{id:long}")]
        [EndpointSummary("Update existing")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, UpdateAuthorRequest request, CancellationToken token)
        {
            var result = await _persistence.UpdateAsync(id, request, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return NoContent();
        }
        [HttpPost]  
        [EndpointSummary("Create new")]
        public async Task<IActionResult> CreateAsync([FromBody]CreateAuthorRequest request, CancellationToken token)
        {
            var result = await _persistence.CreateAsync(request, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return NoContent();
        }
        [HttpDelete("{id:long}")]
        [EndpointSummary("Delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute]long id, CancellationToken token)
        {
            var result = await _persistence.DeleteAsync(id, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return NoContent();
        }
    }
}