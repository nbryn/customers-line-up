using CLup.API.Bookings.Contracts.CreateBooking;
using CLup.API.Bookings.Contracts.DeleteBusinessBooking;
using CLup.API.Bookings.Contracts.DeleteUserBooking;
using CLup.API.TimeSlots.Contracts.GenerateTimeSlots;
using CLup.Application.Businesses;
using CLup.Application.TimeSlots;
using CLup.Domain.Bookings;
using CLup.Domain.Businesses;
using CLup.Domain.TimeSlots;
using CLup.Domain.Users;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public sealed class BookingControllerTests : IntegrationTestsBase
{
    public BookingControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidBusinessAndTimeSlotId_TimeSlotIsNotFull_CreateBookingSucceeds()
    {
        var (_, business) = await CreateUserWithBusiness();
        var timeSlot = await GenerateTimeSlotsAndReturnFirst(business.Id);
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);

        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        var user = await GetUser();
        user.Bookings.Should().HaveCount(1);
        user.Bookings.First().TimeSlotId.Should().Be(timeSlot.Id);
    }

    [Fact]
    public async Task ValidBusinessAndTimeSlotId_BookingExists_CreateBookingFails()
    {
        var (_, business) = await CreateUserWithBusiness();
        var timeSlot = await GenerateTimeSlotsAndReturnFirst(business.Id);
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);

        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);
        var problemDetails = await PostAsyncAndEnsureBadRequest(BookingRoute, createBookingRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(UserErrors.BookingExists.Code);
    }

    [Fact]
    public async Task ValidBusinessAndTimeSlotId_TimeSlotIsFull_CreateBookingFails()
    {
        var (_, business) = await CreateUserWithBusiness( 1);
        var timeSlot = await GenerateTimeSlotsAndReturnFirst(business.Id);
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        await CreateUserAndSetJwtToken("test5@test.com");
        var problemDetails = await PostAsyncAndEnsureBadRequest(BookingRoute, createBookingRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(TimeSlotErrors.NoCapacity.Code);
    }

    [Fact]
    public async Task RequestWithNoIds_CreateBooking_ReturnsBadRequest()
    {
        await CreateUserAndSetJwtToken();
        var request = new CreateBookingRequest(Guid.Empty, Guid.Empty);
        var problemDetails = await PostAsyncAndEnsureBadRequest(BookingRoute, request);

        problemDetails?.Errors.Count.Should().Be(typeof(CreateBookingRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_CreateBooking_ReturnsBusinessNotFound()
    {
        await CreateUserAndSetJwtToken();
        var request = new CreateBookingRequest(Guid.NewGuid(), Guid.NewGuid());
        var problemDetails = await PostAsyncAndEnsureNotFound(BookingRoute, request);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidTimeSlotId_CreateBooking_ReturnsTimeSlotNotFound()
    {
        await CreateUserWithBusiness();
        var business = (await GetBusinessesForCurrentUser()).First();

        var request = new CreateBookingRequest(business.Id, Guid.NewGuid());
        var problemDetails = await PostAsyncAndEnsureNotFound(BookingRoute, request);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(TimeSlotErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithBookingIdAndBookingExist_DeleteUserBooking_BookingIsDeleted()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var timeSlot = await GenerateTimeSlotsAndReturnFirst(business.Id);
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);

        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);
        var user = await GetUser();
        var booking = user.Bookings.First();

        await DeleteAsyncAndEnsureSuccess($"{BookingRoute}/user/{booking.Id}");

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesForCurrentUser()).First();

        updatedUser.Bookings.Should().BeEmpty();
        updatedUser.SentMessages.Should().HaveCount(1);
        updatedUser.SentMessages.First().ReceiverId.Should().Be(business.Id);

        updatedBusiness.Bookings.Should().BeEmpty();
        updatedBusiness.ReceivedMessages.Should().HaveCount(1);
        updatedBusiness.ReceivedMessages.First().SenderId.Should().Be(userId);
    }

    [Fact]
    public async Task RequestWithoutBookingId_DeleteUserBooking_DeleteBookingFails()
    {
        await CreateUserAndSetJwtToken();
        var problemDetails = await DeleteAsyncAndEnsureBadRequest($"{BookingRoute}/user/{Guid.Empty}");

        problemDetails?.Should().NotBeNull();
        problemDetails?.Errors.Count.Should().Be(typeof(DeleteUserBookingRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBookingId_DeleteUserBooking_ReturnsBookingNotFound()
    {
        await CreateUserAndSetJwtToken();
        var problemDetails = await DeleteAsyncAndEnsureNotFound($"{BookingRoute}/user/{Guid.NewGuid()}");

        problemDetails?.Should().NotBeNull();
        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BookingErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithBookingIdAndBookingExist_DeleteBusinessBooking_BookingIsDeleted()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var timeSlot = await GenerateTimeSlotsAndReturnFirst(business.Id);
        var createBookingRequest = new CreateBookingRequest(business.Id, timeSlot.Id);
        await PostAsyncAndEnsureSuccess(BookingRoute, createBookingRequest);

        var user = await GetUser();
        var booking = user.Bookings.First();

        await DeleteAsyncAndEnsureSuccess($"{BookingRoute}/business/{business.Id}?bookingId={booking.Id}");

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesForCurrentUser()).First();

        updatedUser.Bookings.Should().BeEmpty();
        updatedUser.ReceivedMessages.Should().HaveCount(1);
        updatedUser.ReceivedMessages.First().SenderId.Should().Be(business.Id);

        updatedBusiness.Bookings.Should().BeEmpty();
        updatedBusiness.SentMessages.Should().HaveCount(1);
        updatedBusiness.SentMessages.First().ReceiverId.Should().Be(userId);
    }

    [Fact]
    public async Task RequestWithoutIds_DeleteBusinessBooking_DeleteBookingFails()
    {
        await CreateUserAndSetJwtToken();
        var problemDetails = await DeleteAsyncAndEnsureBadRequest($"{BookingRoute}/business/{Guid.Empty}?bookingId={Guid.Empty}");
        problemDetails?.Errors.Count.Should().Be(typeof(DeleteBusinessBookingRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_DeleteBusinessBooking_ReturnsBusinessNotFound()
    {
        await CreateUserAndSetJwtToken();
        var problemDetails = await DeleteAsyncAndEnsureNotFound($"{BookingRoute}/business/{Guid.NewGuid()}?bookingId={Guid.NewGuid()}");
        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidBookingId_DeleteBusinessBooking_ReturnsBookingNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var problemDetails = await DeleteAsyncAndEnsureNotFound($"{BookingRoute}/business/{business.Id}?bookingId={Guid.NewGuid()}");
        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BookingErrors.NotFound.Code);
    }

    private async Task<TimeSlotDto> GenerateTimeSlotsAndReturnFirst(Guid businessId)
    {
        var generateTimeSlotsRequest = new GenerateTimeSlotsRequest(businessId, DateOnly.FromDateTime(DateTime.Today));
        await PostAsyncAndEnsureSuccess(TimeSlotRoute, generateTimeSlotsRequest);
        var businessWithTimeSlots = await GetBusinessAggregate(businessId);

        return businessWithTimeSlots.TimeSlots.First();
    }
}
