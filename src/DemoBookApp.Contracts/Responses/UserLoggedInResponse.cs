namespace DemoBookApp.Contracts.Responses
{
    public record UserLoggedInResponse
    (
        string id,
        string Name,
        string Email,
        string Token
    );
}