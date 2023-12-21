using AutoMapper;
using CLup.Domain.Employees;

namespace CLup.Application.Employees;

public sealed class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.UserId.Value))
            .ForMember(dest => dest.BusinessId, opts => opts.MapFrom(src => src.BusinessId.Value))
            .ForMember(dest => dest.EmployedSince,
                opts => opts.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.PrivateEmail, opts => opts.MapFrom(src => src.User.UserData.Email))
            .ForMember(dest => dest.Business, opts => opts.MapFrom(src => src.Business.BusinessData.Name))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.User.UserData.Name));
    }
}
