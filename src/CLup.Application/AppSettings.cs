namespace CLup.Application;

public sealed class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = null!;

    public string JwtSecretKey { get; set; } = null!;

    public string Url { get; set; } = null!;
}

public sealed class ConnectionStrings
{
    public string ApplicationInsights { get; set; } = null!;

    public string Development { get; set; } = null!;

    public string Production { get; set; } = null!;
}
