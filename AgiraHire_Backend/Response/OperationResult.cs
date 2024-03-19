namespace AgiraHire_Backend.Response
{
    public class OperationResult<T>
    {
        public T Data { get; }
        public string Message { get; }

        public bool Success => Data != null;

        public OperationResult(T data, string message)
        {
            Data = data;
            Message = message;
        }
    }

}
