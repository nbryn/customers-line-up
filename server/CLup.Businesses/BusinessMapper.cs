using AutoMapper;

using CLup.Businesses.DTO;
namespace CLup.Businesses
{
    public class BusinessMapper : Profile
    {
        public BusinessMapper()
        {
            CreateMap<Business, BusinessDTO>()
                .ForMember(b => b.Type, s => s.MapFrom(m => m.Type.ToString()));    

            CreateMap<BusinessRequest, Business>()
                .ForMember(b => b.Type, s => s.MapFrom(m => BusinessType.Parse(typeof(BusinessType), m.Type)));
        }
    }
}
