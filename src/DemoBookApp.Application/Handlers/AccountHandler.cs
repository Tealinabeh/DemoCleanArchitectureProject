using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Contracts.Responses;
using DemoBookApp.Core;
using Microsoft.AspNetCore.Identity;

namespace DemoBookApp.Application.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ITokenService _tokenService;
        private const string UserRole = "User";
        private const string AdminRole = "Admin";

        public AccountHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signinManager;
        }

        public async Task<ResultOf<UserLoggedInResponse>> LogInAsync(LogInUserRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return CreateFailedResult($"Email '{request.Email}' not found.");

            var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                return CreateFailedResult("Incorrect password.");

            var roles = await _userManager.GetRolesAsync(user);

            return ResultOf<UserLoggedInResponse>.CreateSuccessful(
                new UserLoggedInResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    _tokenService.GenerateToken(user, roles)
                )
            );
        }

        public async Task<ResultOf<NewUserCreatedResponse>> RegisterAsync(RegisterUserRequest request)
        {
            var user = request.ToUser();

            var createdUser = await _userManager.CreateAsync(user, request.Password);
            if (!createdUser.Succeeded)
                return CreateFailedResult(createdUser.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, UserRole);

            if (!roleResult.Succeeded)
                return CreateFailedResult(roleResult.Errors);

            return ResultOf<NewUserCreatedResponse>.CreateSuccessful(
                new NewUserCreatedResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    _tokenService.GenerateToken(user, [UserRole])
                )
            );
        }


        public async Task<Result> ChangeRoleAsync(ChangeRoleRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email.ToLower());

            if (user is null)
                return Result.CreateFailed(new InvalidOperationException("User doesn't exist."));

            string normalizedRole = request.RoleName.ToUpper();

            IdentityResult result = request.Operation switch
            {
                ChangeRoleOperation.Add => await _userManager.AddToRoleAsync(user, normalizedRole),
                ChangeRoleOperation.Remove => await _userManager.RemoveFromRoleAsync(user, normalizedRole),
                _ => throw new ArgumentException("Invalid operation type.", nameof(request.Operation))
            };

            return result.Succeeded
                ? Result.CreateSuccessful()
                : Result.CreateFailed(new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description))));
        }
        private ResultOf<NewUserCreatedResponse> CreateFailedResult(IEnumerable<IdentityError> errors)
        {
            string errorMessage = string.Join(", ", errors.Select(e => e.Description));
            return ResultOf<NewUserCreatedResponse>.CreateFailed(new InvalidOperationException(errorMessage));
        }
        private ResultOf<UserLoggedInResponse> CreateFailedResult(string message)
        {
            return ResultOf<UserLoggedInResponse>.CreateFailed(new InvalidOperationException(message));
        }
    }
}