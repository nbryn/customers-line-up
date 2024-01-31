using CLup.API.Contracts.Bookings.CreateBooking;
using CLup.API.Contracts.TimeSlots.GenerateTimeSlots;
using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;

namespace tests.CLup.IntegrationTests.Tests;

public class BookingControllerTests : IntegrationTestsBase
{
    public BookingControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task TimeSlotIsNotFull_And_ValidBusinessAndTimeSlotId_CreateBookingSucceeds()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var generateTimeSlotsRequest = new GenerateTimeSlotsRequest(business.Id, DateTime.Now);
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);
        var businessWithTimeSlots = await GetBusiness(business.Id);
        var timeSlot = businessWithTimeSlots.TimeSlots.First();

        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);
        var user = await GetUser();

        user.Bookings.Count.Should().Be(1);
        user.Bookings.First().TimeSlotId.Should().Be(timeSlot.Id);
    }

    [Fact]
    public async Task BookingExists_CreateBooking_Fails()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var generateTimeSlotsRequest = new GenerateTimeSlotsRequest(business.Id, DateTime.Now);
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);
        var businessWithTimeSlots = await GetBusiness(business.Id);
        var timeSlot = businessWithTimeSlots.TimeSlots.First();

        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);
        var problemDetails = await PostAsyncAndEnsureBadRequest<CreateBookingRequest, ProblemDetails>(BookingRoute, createBookingRequest);

        problemDetails.Errors.Count.Should().Be(1);
        problemDetails.Errors.First().Key.Should().Be(UserErrors.BookingExists.Code);
    }

    [Fact]
    public async Task TimeSlotIsFull_CreateBooking_Fails()
    {
        const string email = "test@test.com";
        var firstUserId = await CreateUserWithBusiness(email, 1);

        var business = (await GetBusinessesByOwner(firstUserId)).First();

        var generateTimeSlotsRequest = new GenerateTimeSlotsRequest(business.Id, DateTime.Now);
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);
        var businessWithTimeSlots = await GetBusiness(business.Id);
        var timeSlot = businessWithTimeSlots.TimeSlots.First();

        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        await CreateUserAndSetJwtToken("test1@test.com");
        var problemDetails = await PostAsyncAndEnsureBadRequest<CreateBookingRequest, ProblemDetails>(BookingRoute, createBookingRequest);

        problemDetails.Errors.Count.Should().Be(1);
        problemDetails.Errors.First().Key.Should().Be(UserErrors.BookingExists.Code);
    }

    [Fact]
    public async Task RequestWithNoIds_CreateBooking_ReturnsBadRequest()
    {
        const string email = "test@test.com";
        await CreateUserAndSetJwtToken(email);
        var request = new CreateBookingRequest(Guid.Empty, Guid.Empty);

        var problemDetails =
            await PostAsyncAndEnsureBadRequest<CreateBookingRequest, ProblemDetails>(BookingRoute, request);

        problemDetails.Errors.Count.Should().Be(typeof(CreateBookingRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_CreateBooking_ReturnsNotFound()
    {
        const string email = "test@test.com";
        await CreateUserAndSetJwtToken(email);

        var request = new CreateBookingRequest(Guid.NewGuid(), Guid.NewGuid());
        var problemDetails =
            await PostAsyncAndEnsureNotFound<CreateBookingRequest, ProblemDetails>(BookingRoute, request);

        problemDetails.Errors.Count.Should().Be(1);
        problemDetails.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidTimeSlotId_CreateBooking_ReturnsNotFound()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var request = new CreateBookingRequest(business.Id, Guid.NewGuid());
        var problemDetails =
            await PostAsyncAndEnsureNotFound<CreateBookingRequest, ProblemDetails>(BookingRoute, request);

        problemDetails.Errors.Count.Should().Be(1);
        problemDetails.Errors.First().Key.Should().Be(TimeSlotErrors.NotFound.Code);
    }
}
