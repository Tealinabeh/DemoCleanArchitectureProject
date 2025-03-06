namespace DemoBookApp.Contracts.Responses
{
    public record NewUserCreatedResponse
    (
        string id,
        string Name,
        string Email,
        string Token
    );
}