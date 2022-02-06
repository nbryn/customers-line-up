using AutoMapper;
using CLup.Application.Commands.Business.Employee.Create;

namespace CLup.Application.Commands.Business.Employee
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<CreateEmployeeCommand, Domain.Businesses.Employees.Employee>();
        }
    }
}