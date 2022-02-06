﻿using AutoMapper;
using CLup.Domain.Business.Employee;
using CLup.Domain.Businesses.Employees;

namespace CLup.Application.Shared.Models.Mapping
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(b => b.EmployedSince, s => s.MapFrom(m => m.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(b => b.PrivateEmail, s => s.MapFrom(m => m.User.UserData.Email))
                .ForMember(b => b.Business, s => s.MapFrom(m => m.Business.BusinessData.Name))
                .ForMember(b => b.Name, s => s.MapFrom(m => m.User.UserData.Name))
                .ForMember(b => b.BusinessId, s => s.MapFrom(m => m.Business.Id));
        }
    }
}