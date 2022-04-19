using System.Linq;
using AutoMapper;
using CLup.Domain.Businesses;

namespace CLup.Application.Businesses
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<Business, BusinessDto>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.Zip, opts => opts.MapFrom(src => src.Address.Zip))
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, opts => opts.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.Longitude, opts => opts.MapFrom(src => src.Coords.Longitude))
                .ForMember(dest => dest.Latitude, opts => opts.MapFrom(src => src.Coords.Latitude))
                .ForMember(dest => dest.Opens, opts => opts.MapFrom(src => src.Opens))
                .ForMember(dest => dest.Closes, opts => opts.MapFrom(src => src.Closes))
                .ForMember(dest => dest.TimeSlotLength, opts => opts.MapFrom(src => src.BusinessData.TimeSlotLength))
                .ForMember(dest => dest.Capacity, opts => opts.MapFrom(src => src.BusinessData.Capacity))
                .ForMember(dest => dest.BusinessHours, opts => opts.MapFrom(src => $"{src.BusinessHours.Start} - {src.BusinessHours.End}"))
                .ForMember(dest => dest.Messages, opts => opts.MapFrom(src => src.ReceivedMessages.Concat(src.SentMessages)));
        }
    }
}