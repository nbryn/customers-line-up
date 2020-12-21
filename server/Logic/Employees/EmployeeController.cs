using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Data;
using Logic.Auth;
using Logic.DTO;
using Logic.Util;

namespace Logic.Employees
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDTOMapper _dtoMapper;

        public EmployeeController(IEmployeeRepository repository, IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
        }

        [HttpGet]
        [Route("business/{businessId}")]
        public async Task<ICollection<EmployeeDTO>> FetchAllEmployeesForBusiness(int businessId)
        {
            IList<Employee> employees = await _repository.FindEmployeesByBusiness(businessId);

            return employees.Select(x => _dtoMapper.ConvertEmployeeToDTO(x)).ToList();
        }

    }
}