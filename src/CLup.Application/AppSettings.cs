namespace CLup.Application;

public sealed class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }

    public string JwtSecretKey { get; set; }

    public string Url { get; set; }
}


public sealed class ConnectionStrings
{
    public string Development { get; set; }
    public string Production { get; set; }
}
