using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Models;
using CLup.Domain.TimeSlots.Events;
using MediatR;

namespace CLup.Application.TimeSlots.Commands.DeleteTimeSlot;

public sealed class TimeSlotDeletedEventHandler : INotificationHandler<DomainEventNotification<TimeSlotDeletedEvent>>
{
    private readonly ICLupRepository _repository;

    public TimeSlotDeletedEventHandler(ICLupRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DomainEventNotification<TimeSlotDeletedEvent> @event,
        CancellationToken cancellationToken)
    {
        var domainEvent = @event.DomainEvent;
        var users = domainEvent.TimeSlot.Bookings.Select(booking => booking.User);
        foreach (var user in users)
        {
            domainEvent.TimeSlot.Business.BookingDeletedMessage(user.Id);
        }

        await _repository.SaveChangesAsync(true, cancellationToken);
    }
}
