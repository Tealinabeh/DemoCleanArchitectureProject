namespace DemoBookApp.Contracts
{
    public interface IResult
    {
        public bool IsSuccess { get; init;}

        public Exception? Exception { get; init; }
    }
}