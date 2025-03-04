using System.Diagnostics.CodeAnalysis;

namespace DemoBookApp.Contracts
{
    public class ResultOf<T> : IResult
    {
        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Exception))]
        public bool IsSuccess { get; init; }
        public T? Value{ get; init; }
        public Exception? Exception { get; init; }

        private ResultOf(bool state, T? value, Exception? exception){
            IsSuccess = state;
            Value = value;
            Exception = exception;
        }
        public static ResultOf<T> CreateSuccessful([DisallowNull] T value){
            return new ResultOf<T>(true, value, null);
        }
        
        public static ResultOf<T> CreateFailed([DisallowNull] Exception exception){
            return new ResultOf<T>(false, default, exception);
        }
    }
}