using CLup.API.Contracts.Businesses.CreateBusiness;
using CLup.API.Contracts.Businesses.UpdateBusiness;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Shared.ValueObjects;

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
        var problemDetails = await PostAsyncAndEnsureBadRequest(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(CreateBusinessRequest).GetProperties().Length);
    }

    [Fact]
    public async Task ValidRequest_UpdateBusinessSucceeds()
    {
        const string email = "test2@test.com";
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();

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
        var updatedBusiness = await GetBusiness(business);

        updatedBusiness.Name.Should().Be(updateBusinessRequest.Name);
        updatedBusiness.Capacity.Should().Be(updateBusinessRequest.Capacity);
        updatedBusiness.TimeSlotLengthInMinutes.Should().Be(updateBusinessRequest.TimeSlotLengthInMinutes);
        updatedBusiness.Type.Should().Be(updateBusinessRequest.Type);
    }

    [Fact]
    public async Task InvalidRequest_UpdateBusinessFails()
    {
        await CreateUserAndSetJwtToken("test3@test.com");
        var emptyRequest = new UpdateBusinessRequest();
        var problemDetails =await PutAsyncAndEnsureBadRequest(BusinessRoute, emptyRequest);

        problemDetails?.Errors.Count.Should().Be(typeof(UpdateBusinessRequest).GetProperties().Length);
    }

    [Theory]
    [InlineData("test4@test.com", 7)]
    [InlineData("test5@test.com", 11)]
    [InlineData("test6@test.com", 61)]
    public async Task RequestWithTimeSlotLength_ThatIsNotDivisibleBy5_UpdateBusinessFails(string email, int timeSlotLengthInMinutes)
    {
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();
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
    [InlineData("test7@test.com", 65, 10, 11)]
    [InlineData("test8@test.com", 125, 12, 14)]
    [InlineData("test9@test.com", 550, 15, 19)]
    public async Task RequestWithTimeSlotLength_ThatExceedsOpeningHours_UpdateBusinessFails(
        string email,
        int timeSlotLengthInMinutes,
        int opens,
        int closes)
    {
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();
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
