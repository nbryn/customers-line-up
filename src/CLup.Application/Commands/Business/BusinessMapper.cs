using AutoMapper;
using CLup.Application.Commands.Business.Create;
using CLup.Application.Commands.Business.Update;
using CLup.Domain.Business;
using CLup.Domain.Businesses;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Application.Commands.Business
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<CreateBusinessCommand, Domain.Businesses.Business>()
                .ForMember(b => b.Id, s => s.Ignore())
                .ForMember(b => b.BusinessData, s => s.MapFrom(c => new BusinessData(c.Name, c.Capacity, c.TimeSlotLength)))
                .ForMember(b => b.Address, s => s.MapFrom(c => new Address(c.Street, c.Zip, c.City)))
                .ForMember(b => b.Coords, s => s.MapFrom(c => new Coords(c.Longitude, c.Latitude)))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(c => new TimeSpan(c.Opens, c.Closes)))
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));

            CreateMap<UpdateBusinessCommand, Domain.Businesses.Business>()
                .ForMember(b => b.BusinessData, s => s.MapFrom(c => new BusinessData(c.Name, c.Capacity, c.TimeSlotLength)))
                .ForMember(b => b.Address, s => s.MapFrom(c => new Address(c.Street, c.Zip, c.City)))
                .ForMember(b => b.Coords, s => s.MapFrom(c => new Coords(c.Longitude, c.Latitude)))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(c => new TimeSpan(c.Opens, c.Closes)))
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));
        }
    }
}

