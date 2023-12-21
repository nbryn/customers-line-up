using Microsoft.AspNetCore.Authorization;

namespace CLup.Application.Auth;

public static class Policies
{
    public const string Admin = "Admin";
    public const string User = "User";
    public static AuthorizationPolicy AdminPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();

    public static AuthorizationPolicy UserPolicy() => new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(User).Build();
}
