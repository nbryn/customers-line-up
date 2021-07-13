using System;

using AutoMapper;

using CLup.Domain;

namespace CLup.Features.Businesses
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<Business, BusinessDTO>()
                .ForMember(b => b.Type, s => s.MapFrom(m => m.Type.ToString()))
                .ForMember(b => b.City, s => s.MapFrom(m => m.Zip.Substring(m.Zip.LastIndexOf(" "))))
                .ForMember(b => b.BusinessHours, s => s.MapFrom(m => $"{m.Opens} - {m.Closes}"));

            CreateMap<CreateBusiness.Command, Business>()
            .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)))
            .ForMember(b => b.OwnerEmail, s => s.MapFrom(m => m.OwnerEmail))
            .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
            .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));

            CreateMap<UpdateBusiness.Command, Business>()
            .ForMember(b => b.Id, s => s.MapFrom(m => m.Id))
            .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)))
            .ForMember(b => b.OwnerEmail, s => s.MapFrom(m => m.OwnerEmail))
            .ForMember(u => u.CreatedAt, s => s.MapFrom(m => DateTime.Now))
            .ForMember(u => u.UpdatedAt, s => s.MapFrom(m => DateTime.Now));
        }
    }
}

