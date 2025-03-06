using System.Net;

namespace DemoBookApp.Contracts.Exceptions
{
    public class QueryArgumentException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
    : BaseException(message, statusCode){}
}