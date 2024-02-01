using CLup.API.Contracts.Businesses.GetAllBusinesses;

namespace tests.CLup.IntegrationTests.Tests;

public sealed class QueryControllerTests : IntegrationTestsBase
{
    public QueryControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetExistingUser_ReturnsCorrectUser()
    {
        const string email = "test10@test.com";
        await CreateUserAndSetJwtToken(email);
        var user = await GetUser();

        user.Should().NotBeNull();
        user.Email.Should().Be(email);
    }

    [Fact]
    public async Task TwoBusinessesWithDifferentOwner_GetAllBusinesses_ReturnsBoth()
    {
        const string firstEmail = "test11@test.com";
        const string secondEmail = "test12@test.com";
        var firstUserId = await CreateUserWithBusiness(firstEmail);
        var secondUserId = await CreateUserWithBusiness(secondEmail);

        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/business/all");

        response.Should().NotBeNull();

        var businessOwnerIds = response.Businesses.Select(business => business.OwnerId).ToList();
        businessOwnerIds.Count.Should().Be(2);
        businessOwnerIds.All(new[] { firstUserId, secondUserId }.Contains).Should().BeTrue();
    }

    [Fact]
    public async Task OwnerWithOneBusiness_TwoBusinessesExist_GetBusinessesByOwner_ReturnsTheRightBusiness()
    {
        const string firstEmail = "test13@test.com";
        const string secondEmail = "test14@test.com";
        await CreateUserWithBusiness(firstEmail);
        var userId = await CreateUserWithBusiness(secondEmail);

        var businesses = await GetBusinessesByOwner(userId);

        businesses.Should().NotBeNull();
        businesses.Count.Should().Be(1);
        businesses.First().OwnerId.Should().Be(userId);
    }

}
