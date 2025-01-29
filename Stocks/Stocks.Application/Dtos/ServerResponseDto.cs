namespace Stocks.Application.Dtos
{
    public class ServerResponseDto<T>
    {
        public T? Data { get; set; }
        public string? Error { get; set; }
        public bool IsSuccess => Error == null;
    }
}