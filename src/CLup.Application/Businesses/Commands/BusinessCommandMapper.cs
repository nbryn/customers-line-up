using AutoMapper;
using CLup.Application.Businesses.Commands.Create;
using CLup.Application.Businesses.Commands.Update;
using CLup.Domain.Businesses;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Application.Businesses.Commands
{
    public class BusinessCommandMapper : Profile
    {
        public BusinessCommandMapper()
        {
            CreateMap<CreateBusinessCommand, Business>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.BusinessData, opts => opts.MapFrom(src => new BusinessData(src.Name, src.Capacity, src.TimeSlotLength)))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new Address(src.Street, src.Zip, src.City)))
                .ForMember(dest => dest.Coords, opts => opts.MapFrom(src => new Coords(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.BusinessHours, opts => opts.MapFrom(src => new TimeSpan(src.Opens, src.Closes)))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => BusinessType.Parse(typeof(BusinessType), src.Type)));

            CreateMap<UpdateBusinessCommand, Business>()
                .ForMember(dest => dest.BusinessData, opts => opts.MapFrom(src => new BusinessData(src.Name, src.Capacity, src.TimeSlotLength)))
                .ForMember(dest => dest.Address, opts => opts.MapFrom(src => new Address(src.Street, src.Zip, src.City)))
                .ForMember(dest => dest.Coords, opts => opts.MapFrom(src => new Coords(src.Longitude, src.Latitude)))
                .ForMember(dest => dest.BusinessHours, opts => opts.MapFrom(src => new TimeSpan(src.Opens, src.Closes)))
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => BusinessType.Parse(typeof(BusinessType), src.Type)));
        }
    }
}

