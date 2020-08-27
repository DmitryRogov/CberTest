using System;

namespace CberTest.Services.Results
{
    public class OperationResult
    {
        public string ErrorMessage { get; }

        public bool IsError { get; }

        protected OperationResult(bool isError, string message = null)
        {
            IsError = isError;
            ErrorMessage = message;
        }

        public static OperationResult Success()
        {
            return new OperationResult(false);
        }

        public static OperationResult Error(string message)
        {
            return new OperationResult(true, message);
        }
    }

    public class OperationResult<T> where T: class
    {
                public string ErrorMessage { get; }

        public bool IsError { get; }
        public T Result { get; }

        private OperationResult(bool isError, T result, string message = null)
        {
            IsError = isError;
            ErrorMessage = message;
            Result = result;
        }

        public static OperationResult<T> Success(T result)
        {
            return new OperationResult<T>(false, result);
        }

        public static OperationResult<T> Error(string message)
        {
            return new OperationResult<T>(true, null, message);
        }
    }
}
