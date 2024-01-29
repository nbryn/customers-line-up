using CLup.API.Contracts.Businesses.FetchAllBusinesses;
using CLup.API.Contracts.Users.FetchUserAggregate;
using CLup.API.Contracts.Users.UsersNotEmployedByBusiness;
using CLup.Application.Businesses;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Util;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[ApiController]
[Route("api/query")]
public class QueryController : AuthorizedControllerBase
{
    private readonly ICLupRepository _repository;

    public QueryController(ICLupRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("user")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchUserAggregateResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FetchUserAggregate()
    {
        var user = await _repository.FetchUserAggregate(GetUserIdFromJwt());
        if (user == null)
        {
            return NotFound(UserErrors.NotFound);
        }

        return Ok(new FetchUserAggregateResponse(new UserDto().FromUser(user)));
    }

    [HttpGet]
    [Route("business/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FetchAllBusinessesResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FetchAllBusinesses()
    {
        var businesses = await _repository.FetchAllBusinesses();

        return Ok(new FetchAllBusinessesResponse(businesses.Select(new BusinessDto().FromBusiness).ToList()));
    }

    [HttpGet]
    [Route("business/types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult FetchBusinessTypes()
    {
        var types = EnumUtil
            .GetValues<BusinessType>()
            .Select(type => type.ToString("G"))
            .ToList();

        return Ok(types);
    }

    [Route("user/notEmployedByBusiness/{businessId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FetchAllUsersNotAlreadyEmployedByBusiness([FromRoute] UsersNotEmployedByBusinessRequest request)
    {
        var businessId = BusinessId.Create(request.BusinessId);
        var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), businessId);
        if (business == null)
        {
            return NotFound(BusinessErrors.NotFound);
        }

        var users = await _repository.FetchUsersNotEmployedByBusiness(businessId);

        return Ok(new UsersNotEmployedByBusinessResponse()
            { BusinessId = request.BusinessId, Users = users.Select(new UserDto().FromUser).ToList() });
    }
}
