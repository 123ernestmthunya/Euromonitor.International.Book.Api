namespace Euromonitor.International.Book.Core;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public Response(bool isSuccess, string message, T data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }
}
