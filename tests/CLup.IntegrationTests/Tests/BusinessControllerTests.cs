using CLup.API.Contracts.Businesses.CreateBusiness;
using CLup.API.Contracts.Businesses.UpdateBusiness;
using CLup.Application.Shared;
using CLup.Domain.Businesses.Enums;

namespace tests.CLup.IntegrationTests.Tests;

public sealed class BusinessControllerTests : IntegrationTestsBase
{
    public BusinessControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_CreateBusinessSucceeds()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);
        userId.Should().NotBeEmpty();
    }

    [Fact]
    public async Task InvalidRequest_CreateBusinessFails()
    {
        const string email = "test1@test.com";
        await CreateUserAndSetJwtToken(email);
        var emptyRequest = new CreateBusinessRequest();
        var problemDetails =
            await PostAsyncAndEnsureBadRequest<CreateBusinessRequest, ProblemDetails>(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(CreateBusinessRequest).GetProperties().Length);
    }

    [Fact]
    public async Task ValidRequest_UpdateBusinessSucceeds()
    {
        const string email = "test2@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var updateBusinessRequest = new UpdateBusinessRequest
        {
            BusinessId = business.Id,
            Name = "Shoppers",
            Capacity = 2,
            TimeSlotLength = 15,
            Type = BusinessType.Hairdresser,
            Zip = business.Zip,
            City = business.City,
            Street = business.Street,
            Longitude = business.Longitude,
            Latitude = business.Latitude,
            Opens = business.Opens,
            Closes = business.Closes,
        };

        await PutAsyncAndEnsureSuccess(BusinessRoute, updateBusinessRequest);
        var updatedBusiness = await GetBusiness(business);

        updatedBusiness.Name.Should().Be(updateBusinessRequest.Name);
        updatedBusiness.Capacity.Should().Be(updateBusinessRequest.Capacity);
        updatedBusiness.TimeSlotLength.Should().Be(updateBusinessRequest.TimeSlotLength);
        updatedBusiness.Type.Should().Be(updateBusinessRequest.Type);
    }

    [Fact]
    public async Task InvalidRequest_UpdateBusinessFails()
    {
        await CreateUserAndSetJwtToken("test3@test.com");
        var emptyRequest = new UpdateBusinessRequest();
        var problemDetails =
            await PutAsyncAndEnsureBadRequest<UpdateBusinessRequest, ProblemDetails>(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(UpdateBusinessRequest).GetProperties().Length);
    }
}
