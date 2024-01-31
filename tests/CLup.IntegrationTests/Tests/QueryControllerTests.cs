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
    public async Task TwoBusinessesWithDifferentOwner_GetAllBusinesses_ReturnsBoth()
    {
        const string firstEmail = "test1@test.com";
        const string secondEmail = "test2@test.com";
        var firstUserId = await CreateUserWithBusiness(firstEmail);
        var secondUserId = await CreateUserWithBusiness(secondEmail);

        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/business/all");

        response.Should().NotBeNull();

        var businessOwnerIds = response.Businesses.Select(business => business.OwnerId).ToList();
        businessOwnerIds.Count.Should().Be(2);
        businessOwnerIds.SequenceEqual(new[] { firstUserId, secondUserId }).Should().BeTrue();
    }

    [Fact]
    public async Task OwnerWithOneBusiness_TwoBusinessesExist_GetBusinessesByOwner_ReturnsTheRightBusiness()
    {
        const string firstEmail = "test1@test.com";
        const string secondEmail = "test2@test.com";
        await CreateUserWithBusiness(firstEmail);
        var userId = await CreateUserWithBusiness(secondEmail);

        var businesses = await GetBusinessesByOwner(userId);

        businesses.Should().NotBeNull();
        businesses.Count.Should().Be(1);
        businesses.First().OwnerId.Should().Be(userId);
    }

}
