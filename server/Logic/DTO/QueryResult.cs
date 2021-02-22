namespace Logic.DTO
{
    public class QueryResult
    {
        public string _message { get; set; }
        public HttpCode _statusCode { get; set; }

        public QueryResult(HttpCode statusCode)
        {
            _statusCode = statusCode;
        }
        public QueryResult(HttpCode statusCode, string message)
        {
            _statusCode = statusCode;
            _message = message;
        }
    }
} 