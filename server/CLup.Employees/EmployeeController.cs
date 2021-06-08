using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Auth;
using CLup.Employees.DTO;
using CLup.Employees.Interfaces;
using CLup.Extensions;

namespace Logic.Employees
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeService _service;
        public EmployeeController(
            IEmployeeRepository repository,
            IEmployeeService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        [Route("business/{businessId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<EmployeeDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAllEmployeesForBusiness(string businessId)
        {
            var response = await _repository.FindEmployeesByBusiness(businessId);

            return this.CreateActionResult<IList<EmployeeDTO>>(response);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> NewEmployee(NewEmployeeRequest request)
        {
            var response = await _service.VerifyNewEmployee(request);

            return this.CreateActionResult(response);
        }

        [HttpDelete]
        [Route("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEmployee(string email, [FromQuery] string businessId)
        {
            var response = await _repository.DeleteEmployee(email, businessId);

            return this.CreateActionResult(response);
       }
    }
}