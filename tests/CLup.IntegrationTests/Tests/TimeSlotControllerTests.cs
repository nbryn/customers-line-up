using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;

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
        int timeSlotLength,
        int addDays)
    {
        await CreateUserWithBusiness(
            email,
            50,
            new TimeOnly(opensHour, opensMinute),
            new TimeOnly(closesHour, closesMinute),
            timeSlotLength);

        var business = (await GetBusinessesForCurrentUser()).First();
        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(addDays)));

        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var updatedBusiness = await GetBusiness(business);
        var expectedNumberOfTimeSlotsGenerated = (closesHour - opensHour) * (60 / timeSlotLength);

        var firstTimeSlot = updatedBusiness.TimeSlots.First();
        updatedBusiness.TimeSlots.Should().HaveCount(expectedNumberOfTimeSlotsGenerated);
        firstTimeSlot.BusinessId.Should().Be(business.Id);
        firstTimeSlot.Date.Should().Be(DateOnly.FromDateTime(DateTime.UtcNow).AddDays(addDays).ToString("dd/MM/yyyy"));
    }

    [Fact]
    public async Task EmptyRequest_GenerateTimeSlotsFails()
    {
        const string email = "test4@test.com";
        await CreateUserAndSetJwtToken(email);

        var emptyRequest = new GenerateTimeSlotsRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest(TimeSlotRoute, emptyRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(GenerateTimeSlotsRequest).GetProperties().Length);
    }
}
