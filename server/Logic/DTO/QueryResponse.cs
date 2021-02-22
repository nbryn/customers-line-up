namespace Logic.DTO

{
    public class QueryResponse<T> : QueryResult
    {
        public T _result { get; set; }

        public QueryResponse(HttpCode statusCode) : base(statusCode)
        {

        }

         public QueryResponse(HttpCode statusCode, string message) : base(statusCode, message)
        {
        }
        public QueryResponse(HttpCode statusCode, T result) : base(statusCode)
        {
            _result = result;
        }

    }
}