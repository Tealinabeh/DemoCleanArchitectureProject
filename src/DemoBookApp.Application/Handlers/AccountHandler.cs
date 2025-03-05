using DemoBookApp.Application.Interfaces;
using DemoBookApp.Contracts;
using DemoBookApp.Contracts.Mappers;
using DemoBookApp.Contracts.Requests;
using DemoBookApp.Contracts.Responses;
using DemoBookApp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email.ToLower());

            if (user is null) 
                return ResultOf<UserLoggedInResponse>.CreateFailed
                ( 
                    new NullDatabaseEntityException("Email not found.")
                );

            var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) 
                return ResultOf<UserLoggedInResponse>.CreateFailed
                ( 
                    new InvalidDataException("Password is incorrect.")
                );

            return ResultOf<UserLoggedInResponse>.CreateSuccessful
                 (
                     new UserLoggedInResponse
                     (
                        user.UserName,
                        user.Email,
                        _tokenService.GenerateToken(user, await _userManager.GetRolesAsync(user))
                     )
                 );
        }

        public async Task<ResultOf<NewUserCreatedResponse>> RegisterAsync(RegisterUserRequest request)
        {
            try
            {
                var user = request.ToUser();

                var createdUser = await _userManager.CreateAsync(user, request.Password);

                if (!createdUser.Succeeded)
                    return ResultOf<NewUserCreatedResponse>.CreateFailed
                    (
                        new InvalidOperationException($"{createdUser.Errors}")
                    );

                var roleResult = await _userManager.AddToRoleAsync(user, UserRole);

                if (!roleResult.Succeeded)
                    return ResultOf<NewUserCreatedResponse>.CreateFailed
                    (
                        new InvalidOperationException($"{createdUser.Errors}")
                    );

                return ResultOf<NewUserCreatedResponse>.CreateSuccessful
                (
                    new NewUserCreatedResponse
                    (
                       user.UserName,
                       user.Email,
                       _tokenService.GenerateToken(user, [UserRole])
                    )
                );
            }
            catch (Exception e)
            {
                return ResultOf<NewUserCreatedResponse>
                            .CreateFailed(e);
            }
        }
    }
}