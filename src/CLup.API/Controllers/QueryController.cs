using CLup.API.Contracts.Businesses;
using CLup.API.Contracts.Businesses.GetBusiness;
using CLup.API.Contracts.Users.GetUser;
using CLup.API.Contracts.Users.UsersNotEmployedByBusiness;
using CLup.API.Extensions;
using CLup.Application.Businesses;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Shared.Util;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using Microsoft.AspNetCore.Mvc;

namespace CLup.API.Controllers;

[ApiController]
[Route("api/query")]
public sealed class QueryController : AuthorizedControllerBase
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
    public Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        var request = new GetBusinessRequest(businessId);
        return ValidateAndContinueOnSuccess<GetBusinessRequest, GetBusinessRequestValidator>(
            request,
            async () =>
            {
                var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), BusinessId.Create(businessId));
                return this.CreateActionResult(
                    Result.FromValue(new GetBusinessResponse(BusinessDto.FromBusiness(business)),
                        BusinessErrors.NotFound));
            });
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

    [Route("user/notEmployedByBusiness/{id:guid}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsersNotAlreadyEmployedByBusiness([FromRoute] Guid businessId)
    {
        var request = new UsersNotEmployedByBusinessRequest(businessId);
        return await ValidateAndContinueOnSuccess<UsersNotEmployedByBusinessRequest,
            UsersNotEmployedByBusinessRequestValidator>(
            request,
            async () =>
            {
                var businessId = BusinessId.Create(request.BusinessId);
                var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), businessId);
                if (business == null)
                {
                    return this.CreateActionResult(Result.BadRequest(new List<Error>() { BusinessErrors.NotFound }));
                }

                var users = await _repository.FetchUsersNotEmployedByBusiness(businessId);
                return this.CreateActionResult(Result.FromValue(new UsersNotEmployedByBusinessResponse()
                {
                    BusinessId = request.BusinessId, Users = users.Select(UserDto.FromUser).ToList()
                }));
            });
    }
}
