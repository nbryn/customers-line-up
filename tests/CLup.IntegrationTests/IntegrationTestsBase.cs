﻿using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CLup.API.Contracts.Auth;
using CLup.API.Contracts.Businesses.CreateBusiness;
using CLup.API.Contracts.Businesses.GetAllBusinesses;
using CLup.API.Contracts.Businesses.GetBusiness;
using CLup.API.Contracts.Users.GetUser;
using CLup.Application.Businesses;
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
    protected string BookingRoute = $"{BaseRoute}/booking";
    protected string BusinessRoute = $"{BaseRoute}/business";
    protected string TimeSlotRoute = $"{BaseRoute}/timeslot";
    protected string QueryRoute = $"{BaseRoute}/query";
    protected string UserRoute = $"{BaseRoute}/user";

    protected IntegrationTestsBase(IntegrationTestWebAppFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task<Guid> CreateUserWithBusiness(string ownerEmail, int capacity = 50)
    {
        var userId = await CreateUserAndSetJwtToken(ownerEmail);
        var business = new BusinessBuilder()
            .WithOwner(UserId.Create(userId))
            .WithBusinessData("Super Brugsen", capacity, 30)
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

        return userId;
    }

    protected async Task<Guid> CreateUserAndSetJwtToken(string email, string? password = null)
    {
        var newUser = new UserBuilder()
            .WithUserData("Peter", email, password ?? "1234")
            .WithAddress("Farum Hovedgade 15", "3520", "Farum")
            .WithCoords(55.8122540, 12.3706760)
            .WithRole(Role.User)
            .Build();

        var registerRequest = new RegisterRequest()
        {
            Email = newUser.UserData.Email,
            Password = newUser.UserData.Password,
            Name = newUser.UserData.Name,
            Zip = newUser.Address.Zip,
            Street = newUser.Address.Street,
            City = newUser.Address.City,
            Longitude = newUser.Coords.Longitude,
            Latitude = newUser.Coords.Latitude,
        };

        var tokenResponse = await PostAsyncAndEnsureSuccess<RegisterRequest, TokenResponse>(
            $"{BaseRoute}/register",
            registerRequest);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.Token);

        var user = await GetUser();
        return user.Id;
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

    protected async Task<BusinessDto> GetBusiness(Guid businessId)
    {
        if (_httpClient.DefaultRequestHeaders.Authorization == null)
        {
            throw new InvalidOperationException("Not authenticated");
        }

        var response = await GetAsyncAndEnsureSuccess<GetBusinessResponse>($"{QueryRoute}/user/business/{businessId}");
        response.Should().NotBeNull();

        return response.Business;
    }

    protected async Task Login(string email, string password)
    {
        var request = new LoginRequest(email, password);
        var response = await PostAsyncAndEnsureSuccess<LoginRequest, TokenResponse>($"{BaseRoute}/login", request);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
    }

    protected async Task<IList<BusinessDto>> GetBusinessesByOwner(Guid ownerId)
    {
        var response = await GetAsyncAndEnsureSuccess<GetAllBusinessesResponse>($"{QueryRoute}/user/businesses");
        response.Should().NotBeNull();

        return response.Businesses;
    }

    protected async Task<TResult?> GetAsyncAndEnsureSuccess<TResult>(string url) =>
        await GetAsyncAndEnsureStatus<TResult>(url, HttpStatusCode.OK);

    protected async Task PostAsyncAndEnsureSuccess<TRequest>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus(url, request, HttpStatusCode.OK);

    protected async Task<TResult?> PostAsyncAndEnsureSuccess<TRequest, TResult>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResult>(url, request, HttpStatusCode.OK);

    protected async Task<TResponse?> PostAsyncAndEnsureNotFound<TRequest, TResponse>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResponse>(url, request, HttpStatusCode.NotFound);

    protected async Task<TResponse?> PostAsyncAndEnsureBadRequest<TRequest, TResponse>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResponse>(url, request, HttpStatusCode.BadRequest);

    protected async Task<TResult?> GetAsyncAndEnsureStatus<TResult>(string url, HttpStatusCode statusCode)
    {
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResult>(content);
    }

    protected async Task<TResponse?> PostAsyncAndEnsureStatus<TResponse>(
        string url,
        HttpStatusCode statusCode)
    {
        var response = await PostAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResponse>(content);
    }

    protected async Task PostAsyncAndEnsureStatus<TRequest>(
        string url,
        TRequest request,
        HttpStatusCode statusCode)
    {
        var response = await PostAsync(url, request);
        response.StatusCode.Should().Be(statusCode);
    }

    protected async Task<TResult?> PostAsyncAndEnsureStatus<TRequest, TResult>(
        string url,
        TRequest request,
        HttpStatusCode statusCode)
    {
        var response = await PostAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResult>(content);
    }

    private async Task<HttpResponseMessage> PostAsync(string url)
    {
        var response = await _httpClient.PostAsync(url, null);
        return response;
    }

    private async Task<HttpResponseMessage> PostAsync<TRequest>(string url, TRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, stringContent);

        return response;
    }
}
