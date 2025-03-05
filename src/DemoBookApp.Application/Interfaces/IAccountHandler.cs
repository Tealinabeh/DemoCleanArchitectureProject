using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Contracts.Responses;

namespace DemoBookApp.Application.Interfaces
{
    public interface IAccountHandler
    {
        public Task<ResultOf<NewUserCreatedResponse>> RegisterAsync(RegisterUserRequest request);
        public Task<ResultOf<UserLoggedInResponse>> LogInAsync(LogInUserRequest request);
    }
}