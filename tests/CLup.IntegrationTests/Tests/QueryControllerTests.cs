using CLup.Application.Users;

namespace tests.CLup.IntegrationTests.Tests;

public class QueryControllerTests : IntegrationTestsBase
{
    private const string Route = "query";

    public QueryControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetExistingUser_ReturnsCorrectUser()
    {
        const string email = "test@test.com";
        await CreateUserAndSetJwtToken(email);
        var user = await GetAsyncAndEnsureSuccess<UserDto>($"{Route}/user");

        Assert.NotNull(user);
        Assert.Equal(email, user.Email);
    }
}
