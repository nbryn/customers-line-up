using CLup.API.Contracts.Bookings.CreateBooking;
using CLup.API.Contracts.Businesses;
using CLup.API.Contracts.Businesses.GetBusiness;
using CLup.API.Contracts.Employees.CreateEmployee;
using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;

namespace tests.CLup.IntegrationTests.Tests;

public sealed class QueryControllerTests : IntegrationTestsBase
{
    public QueryControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetExistingUser_ReturnsCorrectUser()
    {
        const string email = "test10@test.com";
        await CreateUserAndSetJwtToken(email);
        var user = await GetUser();

        user.Should().NotBeNull();
        user.Email.Should().Be(email);
    }

    [Fact]
    public async Task BusinessWithTimeSlotsBookingAndEmployee_GetBusinesses_Succeeds()
    {
        const string employeeEmail = "test8@test.com";
        var employeeUserId = await CreateUserAndSetJwtToken(employeeEmail);
        const string ownerEmail = "test9@test.com";
        await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesForCurrentUser()).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);

        var generateTimeSlotsRequest =
            new GenerateTimeSlotsRequest(business.Id, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);

        var businessWithTimeSlots = await GetBusiness(business);
        var timeSlot = businessWithTimeSlots.TimeSlots.First();
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        var businessWithTimeSlotsBookingAndEmployee = await GetBusiness(business);

        businessWithTimeSlotsBookingAndEmployee.Should().NotBeNull();
        businessWithTimeSlotsBookingAndEmployee.Employees.Should().HaveCount(1);
        businessWithTimeSlotsBookingAndEmployee.Bookings.Should().HaveCount(1);
    }

    [Fact]
    public async Task TwoBusinessesWithDifferentOwner_GetAllBusinesses_ReturnsBoth()
    {
        const string firstEmail = "test11@test.com";
        const string secondEmail = "test12@test.com";
        var firstUserId = await CreateUserWithBusiness(firstEmail);
        var secondUserId = await CreateUserWithBusiness(secondEmail);

        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/business/all");

        response.Should().NotBeNull();

        var businessOwnerIds = response.Businesses.Select(business => business.OwnerId).ToList();
        businessOwnerIds.Count.Should().Be(2);
        businessOwnerIds.TrueForAll(new[] { firstUserId, secondUserId }.Contains).Should().BeTrue();
    }

    [Fact]
    public async Task OwnerWithOneBusiness_TwoBusinessesExist_GetBusinessesByOwner_ReturnsTheRightBusiness()
    {
        const string firstEmail = "test13@test.com";
        const string secondEmail = "test14@test.com";
        await CreateUserWithBusiness(firstEmail);
        var userId = await CreateUserWithBusiness(secondEmail);

        var businesses = await GetBusinessesForCurrentUser();

        businesses.Should().NotBeNull();
        businesses.Should().HaveCount(1);
        businesses.First().OwnerId.Should().Be(userId);
    }
}
