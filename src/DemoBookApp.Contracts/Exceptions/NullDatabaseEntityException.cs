using System.Net;

namespace DemoBookApp.Contracts.Exceptions
{
    public class NullDatabaseEntityException(string message, HttpStatusCode statusCode = HttpStatusCode.NotFound) 
    : BaseException(message, statusCode){}
}