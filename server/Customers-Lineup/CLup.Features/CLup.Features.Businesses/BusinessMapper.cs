using AutoMapper;

using CLup.Domain.Businesses;
using CLup.Domain.Shared.ValueObjects;
using CLup.Features.Businesses.Commands;

namespace CLup.Features.Businesses
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<Business, BusinessDTO>()
                .ForMember(b => b.Type, s => s.MapFrom(m => m.Type.ToString()))
                .ForMember(b => b.Name, s => s.MapFrom(m => m.Name))
                .ForMember(b => b.Zip, s => s.MapFrom(m => m.Address.Zip))
                .ForMember(b => b.City, s => s.MapFrom(m => m.Address.City))
                .ForMember(b => b.Street, s => s.MapFrom(m => m.Address.Street))
                .ForMember(b => b.Longitude, s => s.MapFrom(m => m.Coords.Longitude))
                .ForMember(b => b.Latitude, s => s.MapFrom(m => m.Coords.Latitude))
                .ForMember(b => b.Opens, s => s.MapFrom(m => m.Opens))
                .ForMember(b => b.Closes, s => s.MapFrom(m => m.Closes))
                .ForMember(b => b.TimeSlotLength, s => s.MapFrom(m => m.BusinessData.TimeSlotLength))
                .ForMember(b => b.Capacity, s => s.MapFrom(m => m.BusinessData.Capacity))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(m => $"{m.BusinessHours.Start} - {m.BusinessHours.End}"));

            CreateMap<CreateBusinessCommand, Business>()
                .ForMember(b => b.Id, s => s.Ignore())
                .ForMember(b => b.BusinessData, s => s.MapFrom(c => new BusinessData(c.Name, c.Capacity, c.TimeSlotLength)))
                .ForMember(b => b.Address, s => s.MapFrom(c => new Address(c.Street, c.Zip, c.City)))
                .ForMember(b => b.Coords, s => s.MapFrom(c => new Coords(c.Longitude, c.Latitude)))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(c => new TimeSpan(c.Opens, c.Closes)))
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));

            CreateMap<UpdateBusinessCommand, Business>()
                .ForMember(b => b.BusinessData, s => s.MapFrom(c => new BusinessData(c.Name, c.Capacity, c.TimeSlotLength)))
                .ForMember(b => b.Address, s => s.MapFrom(c => new Address(c.Street, c.Zip, c.City)))
                .ForMember(b => b.Coords, s => s.MapFrom(c => new Coords(c.Longitude, c.Latitude)))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(c => new TimeSpan(c.Opens, c.Closes)))
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));
        }
    }
}

