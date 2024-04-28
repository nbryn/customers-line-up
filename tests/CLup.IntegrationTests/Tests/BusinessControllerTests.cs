using CLup.API.Businesses.Contracts.CreateBusiness;
using CLup.API.Businesses.Contracts.UpdateBusiness;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Shared.ValueObjects;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public sealed class BusinessControllerTests : IntegrationTestsBase
{
    public BusinessControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_CreateBusinessSucceeds()
    {
        var (_, business) = await CreateUserWithBusiness();
        business.Should().NotBeNull();
    }

    [Fact]
    public async Task InvalidRequest_CreateBusinessFails()
    {
        await CreateUserAndSetJwtToken();
        var emptyRequest = new CreateBusinessRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(CreateBusinessRequest).GetProperties().Length);
    }

    [Fact]
    public async Task ValidRequest_UpdateBusinessSucceeds()
    {
        var (_, business) = await CreateUserWithBusiness();
        var updateBusinessRequest = new UpdateBusinessRequest
        {
            BusinessId = business.Id,
            Name = "Shoppers",
            Capacity = 2,
            TimeSlotLengthInMinutes = 15,
            Type = BusinessType.Hairdresser,
            Address = business.Address,
            BusinessHours = business.BusinessHours,
        };

        await PutAsyncAndEnsureSuccess(BusinessRoute, updateBusinessRequest);
        var updatedBusiness = await GetBusinessAggregate(business.Id);

        updatedBusiness.Name.Should().Be(updateBusinessRequest.Name);
        updatedBusiness.Capacity.Should().Be(updateBusinessRequest.Capacity);
        updatedBusiness.TimeSlotLengthInMinutes.Should().Be(updateBusinessRequest.TimeSlotLengthInMinutes);
        updatedBusiness.Type.Should().Be(updateBusinessRequest.Type);
    }

    [Fact]
    public async Task InvalidRequest_UpdateBusinessFails()
    {
        await CreateUserAndSetJwtToken();
        var emptyRequest = new UpdateBusinessRequest();
        var problemDetails =await PutAsyncAndEnsureBadRequest(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(UpdateBusinessRequest).GetProperties().Length);
    }

    [Theory]
    [InlineData( 7)]
    [InlineData( 11)]
    [InlineData( 61)]
    public async Task RequestWithTimeSlotLength_ThatIsNotDivisibleBy5_UpdateBusinessFails(int timeSlotLengthInMinutes)
    {
        var (_, business) = await CreateUserWithBusiness();
        var updateBusinessRequest = new UpdateBusinessRequest
        {
            BusinessId = business.Id,
            Name = business.Name,
            Capacity = business.Capacity,
            TimeSlotLengthInMinutes = timeSlotLengthInMinutes,
            Type = business.Type,
            Address = business.Address,
            BusinessHours = business.BusinessHours,
        };

        var problemDetails =
            await PutAsyncAndEnsureBadRequest(BusinessRoute, updateBusinessRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Value.First().Should().Be(BusinessErrors.InvalidTimeSlotLength.Message);
    }

    [Theory]
    [InlineData( 65, 10, 11)]
    [InlineData( 125, 12, 14)]
    [InlineData( 550, 15, 19)]
    public async Task RequestWithTimeSlotLength_ThatExceedsOpeningHours_UpdateBusinessFails(
        int timeSlotLengthInMinutes,
        int opens,
        int closes)
    {
        var (_, business) = await CreateUserWithBusiness();
        var updateBusinessRequest = new UpdateBusinessRequest
        {
            BusinessId = business.Id,
            Name = business.Name,
            Capacity = business.Capacity,
            TimeSlotLengthInMinutes = timeSlotLengthInMinutes,
            Type = business.Type,
            Address = business.Address,
            BusinessHours = new TimeInterval(new TimeOnly(opens, 0), new TimeOnly(closes, 0))
        };

        var problemDetails =
            await PutAsyncAndEnsureBadRequest(BusinessRoute, updateBusinessRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Value.First().Should().Be(BusinessErrors.TimeSlotLengthExceedsOpeningHours.Message);
    }
}
