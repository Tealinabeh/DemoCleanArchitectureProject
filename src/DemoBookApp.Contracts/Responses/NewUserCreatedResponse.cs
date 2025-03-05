namespace DemoBookApp.Contracts.Responses
{
    public record NewUserCreatedResponse
    (
        string Name,
        string Email,
        string Token
    );
}