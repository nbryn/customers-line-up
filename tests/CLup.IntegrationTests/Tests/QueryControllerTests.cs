using CLup.API.Bookings.Contracts.CreateBooking;
using CLup.API.Businesses.Contracts;
using CLup.API.Employees.Contracts.CreateEmployee;
using CLup.API.TimeSlots.Contracts.GenerateTimeSlots;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public sealed class QueryControllerTests : IntegrationTestsBase
{
    public QueryControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetExistingUser_ReturnsCorrectUser()
    {
        var userId = await CreateUserAndSetJwtToken();
        var user = await GetUser();

        user.Should().NotBeNull();
        user.Id.Should().Be(userId);
    }

    [Fact]
    public async Task BusinessWithTimeSlotsBookingAndEmployee_GetBusinesses_Succeeds()
    {
        var employeeUserId = await CreateUserAndSetJwtToken();
        var (_, business) = await CreateUserWithBusiness();
        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var businessWithTimeSlots = await GetBusinessAggregate(business.Id);
        var timeSlot = businessWithTimeSlots.TimeSlots.First();
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        var businessWithTimeSlotsBookingAndEmployee = await GetBusinessAggregate(business.Id);

        businessWithTimeSlotsBookingAndEmployee.Should().NotBeNull();
        businessWithTimeSlotsBookingAndEmployee.Employees.Should().HaveCount(1);
        businessWithTimeSlotsBookingAndEmployee.Bookings.Should().HaveCount(1);
    }

    [Fact]
    public async Task TwoBusinessesWithDifferentOwner_GetAllBusinesses_ReturnsBoth()
    {
        var (firstUserId, _) = await CreateUserWithBusiness();
        var (secondUserId, _) = await CreateUserWithBusiness();

        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/business/all");

        response.Should().NotBeNull();

        var businessOwnerIds = response.Businesses.Select(business => business.OwnerId).ToList();
        businessOwnerIds.Count.Should().Be(2);
        businessOwnerIds.TrueForAll(new[] { firstUserId, secondUserId }.Contains).Should().BeTrue();
    }

    [Fact]
    public async Task OwnerWithOneBusiness_TwoBusinessesExist_GetBusinessesByOwner_ReturnsTheRightBusiness()
    {
        await CreateUserWithBusiness();
        var (userId, _) = await CreateUserWithBusiness();

        var businesses = await GetBusinessesForCurrentUser();

        businesses.Should().NotBeNull();
        businesses.Should().HaveCount(1);
        businesses.First().OwnerId.Should().Be(userId);
    }
}
