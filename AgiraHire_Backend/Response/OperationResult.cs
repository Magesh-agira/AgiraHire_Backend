namespace AgiraHire_Backend.Response
{
    public class OperationResult<T>
    {
        public T Data { get; }
        public string Message { get; }
        public int ErrorCode { get; } // New property for error code

        public bool Success => ErrorCode == 0;

        public OperationResult(T data, string message, int errorCode = 0)
        {
            Data = data;
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
