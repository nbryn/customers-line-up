using AutoMapper;

using CLup.Businesses.DTO;

namespace CLup.Businesses
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMap<Business, BusinessDTO>()
                .ForMember(b => b.Type, s => s.MapFrom(m => m.Type.ToString()));    

            CreateMap<NewBusinessRequest, Business>()
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));
        }
    }
}
