using AutoMapper;
using CLup.Application.Businesses.Employees.Commands.Create;
using CLup.Domain.Businesses.Employees;

namespace CLup.Application.Businesses.Employees.Commands
{
    public class EmployeeCommandMapper : Profile
    {
        public EmployeeCommandMapper()
        {
            CreateMap<CreateEmployeeCommand, Employee>();
        }
    }
}