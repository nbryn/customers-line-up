namespace CLup.Util
{
    public class ErrorResponse
    {
        public ErrorResponse(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string Code { get; set; }
        public string Description { get; set; }
    }
}