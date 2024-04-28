using CLup.API.Auth;
using CLup.API.Businesses.Contracts;
using CLup.API.Businesses.Contracts.GetBusiness;
using CLup.API.Businesses.Contracts.GetBusinessAggregate;
using CLup.API.Extensions;
using CLup.API.Users.Contracts.GetUser;
using CLup.API.Users.Contracts.UsersNotEmployedByBusiness;
using CLup.Application.Businesses;
using CLup.Application.Shared;
using CLup.Application.Shared.Interfaces;
using CLup.Application.Users;
using CLup.Domain.Businesses;
using CLup.Domain.Businesses.ValueObjects;
using CLup.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using ProblemDetails = CLup.Application.Shared.ProblemDetails;

namespace CLup.API.Queries;

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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
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
    [Route("business/aggregate/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessAggregateResponse))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessAggregate([FromRoute] Guid businessId)
    {
        var request = new GetBusinessAggregateRequest(businessId);
        var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), BusinessId.Create(businessId));

        return this.CreateActionResult(Result.Validate<GetBusinessAggregateRequest, GetBusinessAggregateRequestValidator>(request)
            .Combine(business, [BusinessErrors.NotFound])
            .Combine(new GetBusinessAggregateResponse(BusinessAggregateDto.FromBusiness(business))));
    }

    [HttpGet]
    [Route("business/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessResponse))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusiness([FromRoute] Guid businessId)
    {
        var request = new GetBusinessAggregateRequest(businessId);
        var business = await _repository.FetchBusinessById(BusinessId.Create(businessId));

        return this.CreateActionResult(Result.Validate<GetBusinessAggregateRequest, GetBusinessAggregateRequestValidator>(request)
            .Combine(business, [BusinessErrors.NotFound])
            .Combine(new GetBusinessResponse(BusinessDto.FromBusiness(business))));
    }

    [HttpGet]
    [Route("user/businesses")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBusinessesByOwnerResponse))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBusinessesByOwner()
    {
        var userId = GetUserIdFromJwt();
        var user = await _repository.FetchUserAggregate(userId);
        if (user == null)
        {
            return NotFound(UserErrors.NotFound);
        }

        var businesses = await _repository.FetchBusinessesByOwner(userId);
        return Ok(new GetBusinessesByOwnerResponse(businesses
            .Select(BusinessAggregateDto.FromBusiness)
            .ToList()));
    }

    [HttpGet]
    [Route("business/all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllBusinessesResponse))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var businesses = await _repository.FetchAllBusinesses();
        return Ok(new GetAllBusinessesResponse(businesses
            .Select(BusinessDto.FromBusiness)
            .ToList()));
    }

    [HttpGet]
    [Route("user/notEmployedByBusiness/{businessId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersNotEmployedByBusinessResponse))]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsersNotAlreadyEmployedByBusiness([FromRoute] Guid businessId)
    {
        var request = new UsersNotEmployedByBusinessRequest(businessId);
        var id = BusinessId.Create(request.BusinessId);
        var business = await _repository.FetchBusinessAggregate(GetUserIdFromJwt(), id);
        var users = await _repository.FetchUsersNotEmployedByBusiness(id);

        return this.CreateActionResult(Result.Validate<UsersNotEmployedByBusinessRequest, UsersNotEmployedByBusinessRequestValidator>(request)
            .Combine(business, [BusinessErrors.NotFound])
            .Combine(new UsersNotEmployedByBusinessResponse()
        {
            BusinessId = request.BusinessId, Users = users.Select(UserDto.FromUser).ToList()
        }));
    }
}
