using CLup.Application.TimeSlots;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Application.Businesses;

public sealed class BusinessDto
{
    public Guid Id { get; init; }

    public Guid OwnerId { get; init; }

    public string Name { get; init; }

    public Address Address { get; init; }

    public TimeInterval BusinessHours { get; init; }

    public int TimeSlotLengthInMinutes { get; init; }

    public BusinessType Type { get; init; }

    public int Capacity { get; init; }

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
