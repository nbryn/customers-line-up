using CLup.API.Contracts.TimeSlots.DeleteTimeSlot;
using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;

namespace tests.CLup.IntegrationTests.Tests;

public class TimeSlotControllerTests : IntegrationTestsBase
{
    public TimeSlotControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("test@test.com", 8, 0, 12, 0, 5, 0)]
    [InlineData("test1@test.com", 10, 15, 14, 15, 15, 1)]
    [InlineData("test2@test.com", 12, 30, 18, 30, 30, 2)]
    [InlineData("test3@test.com", 18, 45, 22, 45, 60, 3)]
    public async Task ValidRequest_GenerateTimeSlotsSucceeds(
        string email,
        int opensHour,
        int opensMinute,
        int closesHour,
        int closesMinute,
        int timeSlotLengthInMinutes,
        int addDays)
    {
        await CreateUserWithBusiness(
            email,
            50,
            new TimeOnly(opensHour, opensMinute),
            new TimeOnly(closesHour, closesMinute),
            timeSlotLengthInMinutes);

        var business = (await GetBusinessesForCurrentUser()).First();
        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(addDays)));

        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var updatedBusiness = await GetBusiness(business);
        var expectedNumberOfTimeSlotsGenerated = (closesHour - opensHour) * (60 / timeSlotLengthInMinutes);

        var firstTimeSlot = updatedBusiness.TimeSlots.First();
        updatedBusiness.TimeSlots.Should().HaveCount(expectedNumberOfTimeSlotsGenerated);
        firstTimeSlot.BusinessId.Should().Be(business.Id);
        firstTimeSlot.Date.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow).AddDays(addDays).ToString("dd/MM/yyyy"));
    }

    [Fact]
    public async Task RequestWithDateInThePast_GenerateTimeSlotsFails()
    {
        const string email = "test5@test.com";
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1));
        var problemDetails = await PostAsyncAndEnsureBadRequest(TimeSlotRoute, generateTimeSlotsRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors
            .First().Value
            .First()
            .Should().Be(TimeSlotErrors.TimeSlotCannotBeGeneratedOnDateInThePast.Message);
    }

    [Fact]
    public async Task RequestWithInvalidBusiness_GenerateTimeSlots_ReturnsBusinessNotFound()
    {
        const string email = "test6@test.com";
        await CreateUserAndSetJwtToken(email);

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow));
        var problemDetails = await PostAsyncAndEnsureNotFound(TimeSlotRoute, generateTimeSlotsRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_DeleteTimeSlotSucceeds()
    {
        const string email = "test7@test.com";
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow));
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);
        var businessWithTimeSlots = await GetBusiness(business);
        var timeSlotToBeDeleted = businessWithTimeSlots.TimeSlots.First();

        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{timeSlotToBeDeleted.Id}?businessId={business.Id}";
        await DeleteAsyncAndEnsureSuccess(deleteTimeSlotRoute);

        var updatedBusiness = await GetBusiness(business);
        updatedBusiness.TimeSlots.Should().HaveCount(businessWithTimeSlots.TimeSlots.Count - 1);
        updatedBusiness.TimeSlots.Select(timeSlot => timeSlot.Id).Should().NotContain(timeSlotToBeDeleted.Id);
    }

    [Fact]
    public async Task InvalidRequest_DeleteTimeSlotFails()
    {
        const string email = "test8@test.com";
        await CreateUserAndSetJwtToken(email);

        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.Empty}?businessId={Guid.Empty}";
        var problemDetails = await DeleteAsyncAndEnsureBadRequest(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(typeof(DeleteTimeSlotRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_DeleteTimeSlot_ReturnsTimeSlotNotFound()
    {
        const string email = "test9@test.com";
        await CreateUserAndSetJwtToken(email);

        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.NewGuid()}?businessId={Guid.NewGuid()}";
        var problemDetails = await DeleteAsyncAndEnsureNotFound(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidTimeSlotId_DeleteTimeSlot_ReturnsTimeSlotNotFound()
    {
        const string email = "test10@test.com";
        await CreateUserWithBusiness(email);
        var business = (await GetBusinessesForCurrentUser()).First();

        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.NewGuid()}?businessId={business.Id}";
        var problemDetails = await DeleteAsyncAndEnsureNotFound(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(TimeSlotErrors.NotFound.Code);
    }
}
