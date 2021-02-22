using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using Data;
using Logic.Auth;
using Logic.Context;
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

        private readonly IEmployeeService _service;
        private readonly IDTOMapper _dtoMapper;

        public EmployeeController(IEmployeeRepository repository,
                                  IEmployeeService service, 
                                  IDTOMapper dtoMapper)
        {
            _repository = repository;
            _dtoMapper = dtoMapper;
            _service = service;
        }

        [HttpGet]
        [Route("business/{businessId}")]
        public async Task<IActionResult> FetchAllEmployeesForBusiness(int businessId)
        {
            var response = await _repository.FindEmployeesByBusiness(businessId);

            if (response == null)
            {
                return NotFound();
            }

            var employees = response.Select(x => _dtoMapper.ConvertEmployeeToDTO(x)).ToList();

            return Ok(employees);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> NewEmployee(NewEmployeeRequest request)
        {
            HttpCode response = await _service.VerifyNewEmployee(request);

            return new StatusCodeResult((int)response);
        }

        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] int businessId)
        {
            HttpCode response = await _repository.DeleteEmployee(email, businessId);

            return new StatusCodeResult((int)response);
        }

    }
}