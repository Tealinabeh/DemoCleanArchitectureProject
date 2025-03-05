namespace DemoBookApp.Contracts.Responses
{
    public record UserLoggedInResponse
    (
        string Name,
        string Email,
        string Token
    );
}