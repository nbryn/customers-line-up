using CLup.API.Contracts.Users.UpdateUser;

namespace tests.CLup.IntegrationTests.Tests;

public class UserControllerTests : IntegrationTestsBase
{
    public UserControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_UpdateUserSucceeds()
    {
        const string email = "test@test.com";
        await CreateUserAndSetJwtToken(email);
        var user = await GetUser();
        var updateUserRequest = new UpdateUserRequest()
        {
            Email = "new_email@test.com",
            Name = "Test123",
            Zip = 3520,
            Street = "Temp",
            City = "Hejsa",
            Longitude = user.Longitude,
            Latitude = user.Latitude
        };

        await PutAsyncAndEnsureSuccess($"{UserRoute}/update", updateUserRequest);
        var updatedUser = await GetUser();

        updatedUser.Email.Should().Be(updateUserRequest.Email);
        updatedUser.Name.Should().Be(updateUserRequest.Name);
        updatedUser.Zip.Should().Be(updateUserRequest.Zip);
        updatedUser.Street.Should().Be(updateUserRequest.Street);
        updatedUser.City.Should().Be(updateUserRequest.City);
    }

    [Fact]
    public async Task EmptyRequest_UpdateUserFails()
    {
        const string email = "test1@test.com";
        await CreateUserAndSetJwtToken(email);

        var emptyRequest = new UpdateUserRequest();
        var errorDetails = await PutAsyncAndEnsureBadRequest($"{UserRoute}/update", emptyRequest);

        errorDetails?.Errors.Should().HaveCount(typeof(UpdateUserRequest).GetProperties().Length);
    }
}
