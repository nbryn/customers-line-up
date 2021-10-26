using AutoMapper;

using CLup.Domain;
using CLup.Features.Employees.Commands;

namespace CLup.Features.Employees
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(b => b.EmployedSince, s => s.MapFrom(m => m.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(b => b.PrivateEmail, s => s.MapFrom(m => m.User.UserData.Email))
                .ForMember(b => b.Business, s => s.MapFrom(m => m.Business.BusinessData.Name))
                .ForMember(b => b.Name, s => s.MapFrom(m => m.User.UserData.Name))
                .ForMember(b => b.BusinessId, s => s.MapFrom(m => m.Business.Id));

            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}