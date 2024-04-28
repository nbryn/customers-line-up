using CLup.API.TimeSlots.Contracts.DeleteTimeSlot;
using CLup.API.TimeSlots.Contracts.GenerateTimeSlots;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public class TimeSlotControllerTests : IntegrationTestsBase
{
    public TimeSlotControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData(8, 0, 12, 0, 5, 0)]
    [InlineData(10, 15, 14, 15, 15, 1)]
    [InlineData(12, 30, 18, 30, 30, 2)]
    [InlineData(18, 45, 22, 45, 60, 3)]
    public async Task ValidRequest_GenerateTimeSlotsSucceeds(
        int opensHour,
        int opensMinute,
        int closesHour,
        int closesMinute,
        int timeSlotLengthInMinutes,
        int addDays)
    {
        var (_, business) = await CreateUserWithBusiness(
            50,
            new TimeOnly(opensHour, opensMinute),
            new TimeOnly(closesHour, closesMinute),
            timeSlotLengthInMinutes);

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(addDays)));

        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var updatedBusiness = await GetBusinessAggregate(business.Id);
        var expectedNumberOfTimeSlotsGenerated = (closesHour - opensHour) * (60 / timeSlotLengthInMinutes);

        var firstTimeSlot = updatedBusiness.TimeSlots.First();
        updatedBusiness.TimeSlots.Should().HaveCount(expectedNumberOfTimeSlotsGenerated);
        firstTimeSlot.BusinessId.Should().Be(business.Id);
        firstTimeSlot.Date.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow).AddDays(addDays).ToString("dd/MM/yyyy"));
    }

    [Fact]
    public async Task RequestWithDateInThePast_GenerateTimeSlotsFails()
    {
        var (_, business) =  await CreateUserWithBusiness();
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
        await CreateUserAndSetJwtToken();
        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.UtcNow));
        var problemDetails = await PostAsyncAndEnsureNotFound(TimeSlotRoute, generateTimeSlotsRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_DeleteTimeSlotSucceeds()
    {
        var (_, business) = await CreateUserWithBusiness();
        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow));
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var businessWithTimeSlots = await GetBusinessAggregate(business.Id);
        var timeSlotToBeDeleted = businessWithTimeSlots.TimeSlots.First();

        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{timeSlotToBeDeleted.Id}?businessId={business.Id}";
        await DeleteAsyncAndEnsureSuccess(deleteTimeSlotRoute);

        var updatedBusiness = await GetBusinessAggregate(business.Id);
        updatedBusiness.TimeSlots.Should().HaveCount(businessWithTimeSlots.TimeSlots.Count - 1);
        updatedBusiness.TimeSlots.Select(timeSlot => timeSlot.Id).Should().NotContain(timeSlotToBeDeleted.Id);
    }

    [Fact]
    public async Task InvalidRequest_DeleteTimeSlotFails()
    {
        await CreateUserAndSetJwtToken();
        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.Empty}?businessId={Guid.Empty}";
        var problemDetails = await DeleteAsyncAndEnsureBadRequest(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(typeof(DeleteTimeSlotRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_DeleteTimeSlot_ReturnsTimeSlotNotFound()
    {
        await CreateUserAndSetJwtToken();
        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.NewGuid()}?businessId={Guid.NewGuid()}";
        var problemDetails = await DeleteAsyncAndEnsureNotFound(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidTimeSlotId_DeleteTimeSlot_ReturnsTimeSlotNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var deleteTimeSlotRoute = $"{TimeSlotRoute}/{Guid.NewGuid()}?businessId={business.Id}";
        var problemDetails = await DeleteAsyncAndEnsureNotFound(deleteTimeSlotRoute);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(TimeSlotErrors.NotFound.Code);
    }
}
