using AutoMapper;
using CLup.Application.Commands.Business.Employee.Models;

namespace CLup.Application.Commands.Business.Employee
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<CreateEmployeeCommand, Domain.Business.Employee.Employee>();
        }
    }
}