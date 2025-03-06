using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoBookApp.Api.Controllers
{
    [Route("api/account/my/{userId:Guid}")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserHandler _handler;

        public UserController(IUserHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetFavoriteBooksAsync([FromRoute] Guid userId, CancellationToken token)
        {

            var result = await _handler.GetFavoriteBooksAsync(userId, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return Ok(result.Value);
        }
        [HttpGet("authors")]
        public async Task<IActionResult> GetFavoriteAuthorsAsync([FromRoute] Guid userId, CancellationToken token)
        {
            var result = await _handler.GetFavoriteAuthorsAsync(userId, token);
            if (!result.IsSuccess)
            {
                return result.Exception switch
                {
                    NullDatabaseEntityException => NotFound(result.Exception.Message),
                    _ => StatusCode(500, result.Exception.Message)
                };
            }
            return Ok(result.Value);
        }
        [HttpPost("books/add/{bookId:long}")]
        public async Task<IActionResult> AddBookToFavoritesAsync([FromRoute] Guid userId, [FromRoute] long bookId, CancellationToken token)
        {
            var result = await _handler.AddBookToFavoritesAsync(userId, bookId, token);
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
        [HttpPost("authors/add/{authorId:long}")]
        public async Task<IActionResult> AddAuthorToFavoritesAsync([FromRoute] Guid userId, [FromRoute] long authorId, CancellationToken token)
        {
            var result = await _handler.AddAuthorToFavoritesAsync(userId, authorId, token);
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
        [HttpPost("books/remove/{bookId:long}")]
        public async Task<IActionResult> RemoveBookFromFavoritesAsync([FromRoute] Guid userId, [FromRoute] long bookId, CancellationToken token)
        {
            var result = await _handler.RemoveBookFromFavoritesAsync(userId, bookId, token);
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
        [HttpPost("authors/remove/{authorId:long}")]
        public async Task<IActionResult> RemoveAuthorFromFavoritesAsync([FromRoute] Guid userId, [FromRoute] long authorId, CancellationToken token)
        {
            var result = await _handler.RemoveAuthorFromFavoritesAsync(userId, authorId, token);
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