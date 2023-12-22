using AutoMapper;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Employees;
using CLup.Domain.Users.ValueObjects;

namespace CLup.Application.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeMapper : Profile
{
    public CreateEmployeeMapper()
    {
        CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(dest => dest.Business, opts => opts.MapFrom(src => BusinessId.Create(src.BusinessId)))
            .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => UserId.Create(src.UserId)));
    }
}
