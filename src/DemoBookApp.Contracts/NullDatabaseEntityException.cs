namespace DemoBookApp.Contracts
{
    public class NullDatabaseEntityException(string message) : Exception(message){}
}