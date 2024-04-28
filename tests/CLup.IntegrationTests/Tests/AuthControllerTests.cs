using System.Net;
using CLup.API.Auth.Contracts;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public sealed class AuthControllerTests : IntegrationTestsBase
{
    public AuthControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GivenValidRequests_CanRegisterAndLogin()
    {
        const string password = "1234";
        await CreateUserAndSetJwtToken(password);
        var user = await GetUser();
        await Login(user.Email, password);
    }

    [Fact]
    public async Task GivenInvalidRequest_RegisterFails()
    {
        var request = new RegisterRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest($"{BaseRoute}/register", request);

        problemDetails.Should().NotBeNull();
        problemDetails?.Errors.Count.Should().Be(typeof(RegisterRequest).GetProperties().Length);
    }

    [Fact]
    public async Task GivenInvalidCredentials_LoginFails()
    {
        const string password = "1234";
        await CreateUserAndSetJwtToken(password);
        var user = await GetUser();
        var request = new LoginRequest(user.Email, password + "h");

        await PostAsyncAndEnsureStatus($"{BaseRoute}/login", request, HttpStatusCode.Unauthorized);
    }
}
