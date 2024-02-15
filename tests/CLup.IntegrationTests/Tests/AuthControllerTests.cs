using System.Net;
using CLup.API.Contracts.Auth;
using CLup.Application.Shared;

namespace tests.CLup.IntegrationTests.Tests;

public sealed class AuthControllerTests : IntegrationTestsBase
{
    public AuthControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GivenValidRequests_CanRegisterAndLogin()
    {
        const string email = "test@test.com";
        const string password = "1234";

        await CreateUserAndSetJwtToken(email, password);
        await Login(email, password);
    }

    [Fact]
    public async Task GivenInvalidRequest_RegisterFails()
    {
        var request = new RegisterRequest();
        var response =
            await PostAsyncAndEnsureBadRequest<RegisterRequest, ProblemDetails>($"{BaseRoute}/register", request);

        response.Should().NotBeNull();
        response?.Errors.Count.Should().Be(typeof(RegisterRequest).GetProperties().Length);
    }

    [Fact]
    public async Task GivenInvalidCredentials_LoginFails()
    {
        const string email = "test1@test.com";
        const string password = "1234";

        await CreateUserAndSetJwtToken(email, password);
        var request = new LoginRequest(email, password + "h");

        await PostAsyncAndEnsureStatus($"{BaseRoute}/login", request, HttpStatusCode.Unauthorized);
    }
}
