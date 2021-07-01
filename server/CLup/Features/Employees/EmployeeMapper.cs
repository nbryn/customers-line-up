using AutoMapper;

namespace CLup.Employees
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(b => b.EmployedSince, s => s.MapFrom(m => m.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(b => b.PrivateEmail, s => s.MapFrom(m => m.UserEmail))
                .ForMember(b => b.Name, s => s.MapFrom(m => m.User.Name));
        }
    }
}