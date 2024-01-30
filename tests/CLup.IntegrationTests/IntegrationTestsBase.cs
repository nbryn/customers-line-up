using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CLup.API.Contracts.Auth;
using CLup.API.Contracts.Businesses.CreateBusiness;
using CLup.API.Contracts.Users.GetUser;
using CLup.Application.Users;
using CLup.Domain.Businesses.Enums;
using CLup.Domain.Users.Enums;
using CLup.Domain.Users.ValueObjects;
using CLup.Infrastructure.Persistence.Seed.Builders;
using Newtonsoft.Json;

namespace tests.CLup.IntegrationTests;

public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly HttpClient _httpClient;
    protected static string BaseRoute = "api";
    protected string QueryRoute = $"{BaseRoute}/query";
    protected string UserRoute = $"{BaseRoute}/user";
    protected string BusinessRoute = $"{BaseRoute}/business";

    protected IntegrationTestsBase(IntegrationTestWebAppFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<Guid> CreateUserWithBusiness(string ownerEmail)
    {
        await CreateUserAndSetJwtToken(ownerEmail);
        var user = await GetUser();

        var business = new BusinessBuilder()
            .WithOwner(UserId.Create(user.Id))
            .WithBusinessData("Super Brugsen", 50, 30)
            .WithBusinessHours(10.00, 22.00)
            .WithAddress("Ryttergårdsvej 10", "3520", "Farum")
            .WithCoords(55.8137419, 12.3935222)
            .WithType(BusinessType.Supermarket)
            .Build();

        var request = new CreateBusinessRequest
        {
            Name = business.BusinessData.Name,
            Capacity = business.BusinessData.Capacity,
            TimeSlotLength = business.BusinessData.TimeSlotLength,
            Zip = business.Address.Zip,
            City = business.Address.City,
            Street = business.Address.Street,
            Longitude = business.Coords.Longitude,
            Latitude = business.Coords.Latitude,
            Opens = business.BusinessHours.Start,
            Closes = business.BusinessHours.End
        };

        await PostAsyncAndEnsureSuccess(BusinessRoute, request);

        return user.Id;
    }

    protected async Task CreateUserAndSetJwtToken(string email, string? password = null)
    {
        var user = new UserBuilder()
            .WithUserData("Peter", email, password ?? "1234")
            .WithAddress("Farum Hovedgade 15", "3520", "Farum")
            .WithCoords(55.8122540, 12.3706760)
            .WithRole(Role.User)
            .Build();

        var registerRequest = new RegisterRequest()
        {
            Email = user.UserData.Email,
            Password = user.UserData.Password,
            Name = user.UserData.Name,
            Zip = user.Address.Zip,
            Street = user.Address.Street,
            City = user.Address.City,
            Longitude = user.Coords.Longitude,
            Latitude = user.Coords.Latitude,
        };

        var tokenResponse = await PostAsyncAndEnsureSuccess<RegisterRequest, TokenResponse>(
            $"{BaseRoute}/register",
            registerRequest);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);
    }

    protected async Task<UserDto> GetUser()
    {
        if (_httpClient.DefaultRequestHeaders.Authorization == null)
        {
            throw new InvalidOperationException("Not authenticated");
        }

        var response = await GetAsyncAndEnsureSuccess<GetUserResponse>($"{QueryRoute}/user");
        response.Should().NotBeNull();

        return response.User;
    }

    protected async Task Login(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await PostAsyncAndEnsureSuccess<LoginRequest, TokenResponse>($"{BaseRoute}/login", request);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
    }

    protected async Task<TResult?> GetAsyncAndEnsureSuccess<TResult>(string url) =>
        await GetAsyncAndEnsureStatus<TResult>(url, HttpStatusCode.OK);

    protected async Task<TResult?> GetAsyncAndEnsureNotFound<TResult>(string url) =>
        await GetAsyncAndEnsureStatus<TResult>(url, HttpStatusCode.NotFound);

    protected async Task PostAsyncAndEnsureSuccess<TRequest>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus(url, request, HttpStatusCode.OK);

    protected async Task<TResult?> PostAsyncAndEnsureSuccess<TRequest, TResult>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResult>(url, request, HttpStatusCode.OK);

    protected async Task<TResponse?> PostAsyncAndEnsureBadRequest<TRequest, TResponse>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResponse>(url, request, HttpStatusCode.BadRequest);

    private async Task<TResult?> GetAsyncAndEnsureStatus<TResult>(string url, HttpStatusCode statusCode)
    {
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResult>(content);
    }

    protected async Task PostAsyncAndEnsureStatus<TRequest>(
        string url,
        TRequest request,
        HttpStatusCode statusCode)
    {
        var response = await PostAsync(url, request);
        response.StatusCode.Should().Be(statusCode);
    }

    private async Task<TResult?> PostAsyncAndEnsureStatus<TRequest, TResult>(
        string url,
        TRequest request,
        HttpStatusCode statusCode)
    {
        var response = await PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResult>(content);
    }

    private async Task<HttpResponseMessage> PostAsync<TRequest>(string url, TRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, stringContent);

        return response;
    }
}
