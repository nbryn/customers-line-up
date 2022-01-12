using AutoMapper;
using CLup.Domain.Business;

namespace CLup.Application.Shared.Models.Mapping
{
    public class BusinessDtoMapper : Profile
    {
        public BusinessDtoMapper()
        {
            CreateMap<Business, BusinessDto>()
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
                .ForMember(b => b.BusinessHours,
                    s => s.MapFrom(m => $"{m.BusinessHours.Start} - {m.BusinessHours.End}"));
        }
    }
}