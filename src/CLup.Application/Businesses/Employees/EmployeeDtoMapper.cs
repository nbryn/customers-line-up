using AutoMapper;

namespace CLup.Application.Businesses.Employees.Queries
{
    public class EmployeeDtoMapper : Profile
    {
        public EmployeeDtoMapper()
        {
            CreateMap<Domain.Businesses.Employees.Employee, EmployeeDto>()
                .ForMember(b => b.EmployedSince, s => s.MapFrom(m => m.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(b => b.PrivateEmail, s => s.MapFrom(m => m.User.UserData.Email))
                .ForMember(b => b.Business, s => s.MapFrom(m => m.Business.BusinessData.Name))
                .ForMember(b => b.Name, s => s.MapFrom(m => m.User.UserData.Name))
                .ForMember(b => b.BusinessId, s => s.MapFrom(m => m.Business.Id));
        }
    }
}