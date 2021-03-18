namespace CLup.Util

{
    public class ServiceResponse
    {
        public string _message { get; set; }
        public HttpCode _statusCode { get; set; }

        public bool HaveErrors => ((int)_statusCode).ToString().StartsWith("4");

        public ServiceResponse()
        {

        }

        public ServiceResponse(HttpCode statusCode)
        {
            _statusCode = statusCode;
        }
        public ServiceResponse(HttpCode statusCode, string message)
        {
            _statusCode = statusCode;
            _message = message;
        }
    }
    public class ServiceResponse<T> : ServiceResponse
    {
        public T _response { get; set; }

        public ServiceResponse() : base()
        {

        }
        public ServiceResponse(HttpCode statusCode) : base(statusCode)
        {

        }

        public ServiceResponse(HttpCode statusCode, string message) : base(statusCode, message)
        {
        }
        public ServiceResponse(HttpCode statusCode, T response) : base(statusCode)
        {
            _response = response;
        }

    }
}