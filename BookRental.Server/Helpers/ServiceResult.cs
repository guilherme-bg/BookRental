namespace BookRental.Server.Helpers
{
    public class ServiceResult
    {
        public bool IsSuccess { get; protected set; }
        public string? ErrorMessage { get; protected set; }


        public static ServiceResult Failure(string? errorMessage)
        {
            return new ServiceResult { IsSuccess = false, ErrorMessage = errorMessage };
        }

        public static ServiceResult Success()
        {
            return new ServiceResult { IsSuccess = true };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; private set; }

        public static new ServiceResult<T> Failure(string? errorMessage)
        {
            return new ServiceResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
        }

        public static ServiceResult<T> Success(T data)
        {
            return new ServiceResult<T> { IsSuccess = true, Data = data };
        }
    }
}
