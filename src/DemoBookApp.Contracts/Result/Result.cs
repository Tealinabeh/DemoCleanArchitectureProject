using System.Diagnostics.CodeAnalysis;

namespace DemoBookApp.Contracts
{
    public class Result : IResult
    {
        [MemberNotNullWhen(false, nameof(Exception))]
        public bool IsSuccess { get; init; }
        public Exception? Exception { get; init; }

        private Result(bool state, Exception? exception){
            IsSuccess = state;
            Exception = exception;
        }

        public static Result CreateSuccessful(){
            return new Result(true, null);
        }
        
        public static Result CreateFailed([DisallowNull] Exception exception){
            return new Result(false, exception);
        }
    }
}