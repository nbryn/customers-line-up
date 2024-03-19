using CLup.API.Contracts.Users.UpdateUser;
using CLup.Domain.Shared.ValueObjects;

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
            Address = new Address("Temp", 3520, "Hejsa", user.Address.Coords)
        };

        await PutAsyncAndEnsureSuccess($"{UserRoute}/update", updateUserRequest);
        var updatedUser = await GetUser();

        updatedUser.Email.Should().Be(updateUserRequest.Email);
        updatedUser.Name.Should().Be(updateUserRequest.Name);
        updatedUser.Address.Should().Be(updateUserRequest.Address);
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
