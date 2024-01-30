using CLup.API.Contracts.Businesses.GetAllBusinesses;

namespace tests.CLup.IntegrationTests.Tests;

public class QueryControllerTests : IntegrationTestsBase
{
    public QueryControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetExistingUser_ReturnsCorrectUser()
    {
        const string email = "test@test.com";
        await CreateUserAndSetJwtToken(email);
        var user = await GetUser();

        user.Should().NotBeNull();
        user.Email.Should().Be(email);
    }

    [Fact]
    public async Task BusinessExists_GetAllBusinesses_ReturnsBusiness()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);

        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/business/all");

        response.Should().NotBeNull();
        response.Businesses.Count.Should().Be(1);
        response.Businesses.First().OwnerId.Should().Be(userId);
    }

}
