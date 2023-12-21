using System;
using AutoMapper;
using CLup.Application.Businesses.Commands.CreateBusiness;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared.ValueObjects;
using TimeSpan = CLup.Domain.Shared.ValueObjects.TimeSpan;

namespace CLup.Application.Businesses.Commands.UpdateBusiness;

public sealed class UpdateBusinessMapper : Profile
{
    public UpdateBusinessMapper()
    {
        CreateMap<CreateBusinessCommand, Business>()
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.BusinessData,opts => opts.MapFrom(src => new BusinessData(src.Name, src.Capacity, src.TimeSlotLength)))
            .ForMember(dest => dest.Address,opts => opts.MapFrom(src => new Address(src.Street, src.Zip, src.City)))
            .ForMember(dest => dest.Coords, opts => opts.MapFrom(src => new Coords(src.Longitude, src.Latitude)))
            .ForMember(dest => dest.BusinessHours, opts => opts.MapFrom(src => new TimeSpan(src.Opens, src.Closes)))
            .ForMember(dest => dest.Type, opts => opts.MapFrom(src => Enum.Parse(typeof(BusinessType), src.Type)));
    }
}
