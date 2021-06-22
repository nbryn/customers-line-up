namespace CLup.Util
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorResponse(string code, string description)
        {
            Code = code;
            Description = description;
        }

    }
}