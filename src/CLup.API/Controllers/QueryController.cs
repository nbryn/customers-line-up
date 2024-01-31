using CLup.API.Contracts.Businesses;
using CLup.API.Contracts.Businesses.GetAllBusinesses;
using CLup.API.Contracts.Businesses.GetBusiness;
using CLup.API.Contracts.Users.GetUser;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser()
    {
        var user = await _repository.FetchUserAggregate(GetUserIdFromJwt());
        if (user == null)
        {
            return NotFound(UserErrors.NotFound);
        }

        return Ok(new GetUserResponse(UserDto.FromUser(user)));
    }

    [HttpGet]
    [Route("user/business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusiness([FromRoute] GetBusinessRequest request)
    {
        var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), BusinessId.Create(request.BusinessId));
        if (business == null)
        {
            return NotFound(BusinessErrors.NotFound);
        }

        return Ok(new GetBusinessResponse(BusinessDto.FromBusiness(business)));
    }

    [HttpGet]
    [Route("user/businesses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUserResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessesByOwner()
    {
        var userId = GetUserIdFromJwt();
        var user = await _repository.FetchUserAggregate(userId);
        if (user == null)
        {
            return NotFound(UserErrors.NotFound);
        }

        var businesses = await _repository.FetchBusinessesByOwner(userId);

        return Ok(new GetBusinessesByOwnerResponse(businesses.Select(BusinessDto.FromBusiness).ToList()));
    }

    [HttpGet]
    [Route("business/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllBusinessesResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var businesses = await _repository.FetchAllBusinesses();

        return Ok(new GetAllBusinessesResponse(businesses.Select(BusinessDto.FromBusiness).ToList()));
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

    [Route("user/notEmployedByBusiness/{businessId:guid}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsersNotAlreadyEmployedByBusiness(
        [FromRoute] UsersNotEmployedByBusinessRequest request)
    {
        var businessId = BusinessId.Create(request.BusinessId);
        var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), businessId);
        if (business == null)
        {
            return NotFound(BusinessErrors.NotFound);
        }

        var users = await _repository.FetchUsersNotEmployedByBusiness(businessId);

        return Ok(new UsersNotEmployedByBusinessResponse()
        {
            BusinessId = request.BusinessId, Users = users.Select(UserDto.FromUser).ToList()
        });
    }
}
