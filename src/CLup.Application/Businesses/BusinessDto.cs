using CLup.Application.TimeSlots;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Application.Businesses;

public sealed class BusinessDto
{
    public required Guid Id { get; init; }

    public required Guid OwnerId { get; init; }

    public required string Name { get; init; }

    public required Address Address { get; init; }

    public required TimeInterval BusinessHours { get; init; }

    public required int TimeSlotLengthInMinutes { get; init; }

    public required BusinessType Type { get; init; }

    public required int Capacity { get; init; }

    public required IList<TimeSlotDto> TimeSlots { get; init; }

    public static BusinessDto FromBusiness(Business business)
    {
        return new BusinessDto()
        {
            Id = business.Id.Value,
            OwnerId = business.OwnerId.Value,
            Name = business.BusinessData.Name,
            Address = business.Address,
            BusinessHours = business.BusinessHours,
            Capacity = business.BusinessData.Capacity,
            TimeSlotLengthInMinutes = business.BusinessData.TimeSlotLengthInMinutes,
            Type = business.Type,
            TimeSlots = business.TimeSlots.Select(TimeSlotDto.FromTimeSlot).ToList(),
        };
    }
}
