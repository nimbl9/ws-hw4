namespace BookCatalogApi.Models
{
    public class ReturnResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ReturnResult<T> Ok(T data, string message = "операция выполнена успешно")
        {
            return new ReturnResult<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ReturnResult<T> Fail(string message)
        {
            return new ReturnResult<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}