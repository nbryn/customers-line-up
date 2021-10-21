using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CLup.Features.Auth;
using CLup.Features.Businesses.Commands;
using CLup.Features.Businesses.Queries;
using CLup.Features.Extensions;

namespace CLup.Features.Businesses
{
    [ApiController]
    [Authorize(Policy = Policies.User)]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BusinessController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> NewBusiness([FromBody] CreateBusinessCommand command)
        {

            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            command.OwnerEmail = ownerEmail;

            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("owner")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BusinessesByOwner()
        {
            string ownerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new BusinessesByOwnerQuery(ownerEmail));

            return this.CreateActionResult(result);
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBusinessData([FromBody] UpdateBusinessCommand command)
        {
            command.OwnerEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _mediator.Send(command);

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BusinessDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> FetchAll()
        {
            var result = await _mediator.Send(new AllBusinessesQuery());

            return this.CreateActionResult(result);
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FetchBusinessTypes()
        {
            return Ok(await _mediator.Send(new BusinessTypesQuery()));
        }
    }
}