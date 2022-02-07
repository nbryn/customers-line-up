using AutoMapper;
using CLup.Application.Businesses.Employees.Commands.Create;

namespace CLup.Application.Businesses.Employees.Commands
{
    public class EmployeeCommandMapper : Profile
    {
        public EmployeeCommandMapper()
        {
            CreateMap<CreateEmployeeCommand, Domain.Businesses.Employees.Employee>();
        }
    }
}