using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;

namespace tests.CLup.IntegrationTests.Tests;

public class TimeSlotControllerTests : IntegrationTestsBase
{
    public TimeSlotControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("test@test.com", 8, 12, 60, 0)]
    [InlineData("test1@test.com", 10, 16, 30, 1)]
    [InlineData("test2@test.com", 12, 20, 20, 2)]
    [InlineData("test3@test.com", 17.30, 18.15, 5, 3)]
    public async Task ValidRequest_GenerateTimeSlotsSucceeds(
        string email,
        double opens,
        double closes,
        int timeSlotLength,
        int addDays)
    {
        var userId = await CreateUserWithBusiness(email, 50, opens, closes, timeSlotLength);
        var business = (await GetBusinessesByOwner(userId)).First();
        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.Now.AddDays(addDays)));

        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var updatedBusiness = await GetBusiness(business);
        var expectedNumberOfTimeSlotsGenerated =
            Convert.ToInt32((closes - opens) * Convert.ToDouble(60 / timeSlotLength));
        updatedBusiness.TimeSlots.Should().HaveCount(expectedNumberOfTimeSlotsGenerated);
    }
}
