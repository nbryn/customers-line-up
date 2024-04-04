using CLup.API.Contracts.Businesses;
using CLup.API.Contracts.Businesses.GetBusiness;
using CLup.API.Contracts.Users.GetUser;
using CLup.API.Contracts.Users.UsersNotEmployedByBusiness;
using CLup.API.Extensions;
using CLup.Application.Businesses;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Shared;
using CLup.Domain.Users;
using Microsoft.AspNetCore.Mvc;

using ProblemDetails = CLup.Application.Shared.ProblemDetails;

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
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
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
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessResponse))]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        var request = new GetBusinessRequest(businessId);
        return ValidateAndContinueOnSuccess<GetBusinessRequest, GetBusinessRequestValidator>(
            request,
            async () =>
            {
                var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), BusinessId.Create(businessId));
                return this.CreateActionResult(
                    Result.FromValue(new GetBusinessResponse(BusinessDto.FromBusiness(business, true)),
                        BusinessErrors.NotFound));
            });
    }

    [HttpGet]
    [Route("user/businesses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessesByOwnerResponse))]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessesByOwner()
    {
        var userId = GetUserIdFromJwt();
        var user = await _repository.FetchUserAggregate(userId);
        if (user == null)
        {
            return NotFound(UserErrors.NotFound);
        }

        var businesses = await _repository.FetchBusinessesByOwner(userId);

        return Ok(new GetBusinessesByOwnerResponse(businesses.Select(business => BusinessDto.FromBusiness(business, true)).ToList()));
    }

    [HttpGet]
    [Route("business/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllBusinessesResponse))]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var businesses = await _repository.FetchAllBusinesses();

        return Ok(new GetAllBusinessesResponse(businesses.Select(business => BusinessDto.FromBusiness(business, false)).ToList()));
    }


    [HttpGet]
    [Route("user/notEmployedByBusiness/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
    [ProducesResponseType((typeof(ProblemDetails)), StatusCodes.Status404NotFound)]
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
                    return this.CreateActionResult(Result.BadRequest([BusinessErrors.NotFound]));
                }

                var users = await _repository.FetchUsersNotEmployedByBusiness(businessId);
                return this.CreateActionResult(Result.Ok(new UsersNotEmployedByBusinessResponse()
                {
                    BusinessId = request.BusinessId, Users = users.Select(UserDto.FromUser).ToList()
                }));
            });
    }
}
