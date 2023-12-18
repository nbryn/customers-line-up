using AutoMapper;
using CLup.Domain.Employees;

namespace CLup.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeMapper : Profile
    {
        public CreateEmployeeMapper()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}