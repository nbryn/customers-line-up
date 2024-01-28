using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CLup.Application.Auth;
using CLup.Application.Auth.Commands.Register;
using CLup.Domain.Users.Enums;
using CLup.Infrastructure.Persistence.Seed.Builders;
using Newtonsoft.Json;

namespace tests.CLup.IntegrationTests;

public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly HttpClient _httpClient;
    private const string Route = "api";

    protected IntegrationTestsBase(IntegrationTestWebAppFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    protected async Task CreateUserAndSetJwtToken(string email)
    {
        var user = new UserBuilder()
            .WithUserData("Peter", email, "1234")
            .WithAddress("Farum Hovedgade 15", "3520", "Farum")
            .WithCoords(55.8122540, 12.3706760)
            .WithRole(Role.User)
            .Build();

        var registerCommand = new RegisterCommand()
        {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            Zip = user.Address.Zip,
            Street = user.Address.Street,
            City = user.Address.City,
            Longitude = user.Coords.Longitude,
            Latitude = user.Coords.Latitude,
        };

        var tokenResponse = await PostAsyncAndEnsureSuccess<RegisterCommand, TokenResponse>(
            $"{Route}/register",
            registerCommand);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse?.Token);
    }

    protected async Task<TResult?> GetAsyncAndEnsureSuccess<TResult>(string url) =>
        await GetAsyncAndEnsureStatus<TResult>(url, HttpStatusCode.OK);

    protected async Task<TResult?> GetAsyncAndEnsureNotFound<TResult>(string url) =>
        await GetAsyncAndEnsureStatus<TResult>(url, HttpStatusCode.NotFound);

    private async Task<TResult?> GetAsyncAndEnsureStatus<TResult>(string url, HttpStatusCode statusCode)
    {
        var response = await _httpClient.GetAsync($"{Route}/{url}");
        var content = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(statusCode);

        return JsonConvert.DeserializeObject<TResult>(content);
    }

    public async Task<TResult?> PostAsyncAndEnsureSuccess<TRequest, TResult>(string url, TRequest request) =>
        await PostAsyncAndEnsureStatus<TRequest, TResult>(url, request, HttpStatusCode.OK);

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
