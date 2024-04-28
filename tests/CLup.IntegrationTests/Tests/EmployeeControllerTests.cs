using CLup.API.Employees.Contracts.CreateEmployee;
using CLup.API.Employees.Contracts.DeleteEmployee;
using CLup.Application.Businesses;
using CLup.Domain.Businesses;
using CLup.Domain.Employees;
using CLup.Domain.Users;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public class EmployeeControllerTests : IntegrationTestsBase
{
    public EmployeeControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_CreateEmployeeSucceeds()
    {
        var employeeUserId = await CreateUserAndSetJwtToken();
        var (_, business) = await CreateUserWithBusiness();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);

        var updatedBusiness = await GetBusinessAggregate(business.Id);
        updatedBusiness.Employees.Should().HaveCount(1);
        updatedBusiness.Employees.First().UserId.Should().Be(employeeUserId);
    }

    [Fact]
    public async Task RequestWhereUserIsOwner_CreateEmployee_ReturnsOwnerCantBeEmployee()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, userId);
        var problemDetails = await PostAsyncAndEnsureBadRequest(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(EmployeeErrors.OwnerCannotBeEmployee.Code);
    }

    [Fact]
    public async Task RequestWithoutIds_CreateEmployeeFails()
    {
        await CreateUserAndSetJwtToken();
        var createEmployeeRequest = new CreateEmployeeRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(CreateEmployeeRequest).GetProperties().Length - 1);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_CreateEmployee_ReturnsBusinessNotFound()
    {
        var employeeUserId = await CreateUserAndSetJwtToken();
        await CreateUserAndSetJwtToken();

        var createEmployeeRequest = new CreateEmployeeRequest(Guid.NewGuid(), employeeUserId);
        var problemDetails = await PostAsyncAndEnsureNotFound(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidUserId_CreateEmployee_ReturnsUserNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, Guid.NewGuid());
        var problemDetails = await PostAsyncAndEnsureNotFound(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(UserErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_DeleteEmployeeSucceeds()
    {
        var employeeUserId = await CreateUserAndSetJwtToken();
        var (_, business) = await CreateUserWithBusiness();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);
        var businessWithEmployee = await GetBusinessAggregate(business.Id);
        var employee = businessWithEmployee.Employees.First();

        await DeleteAsyncAndEnsureSuccess($"{EmployeeRoute}/{employee.Id}?businessId={business.Id}");

        var businessWithoutEmployee = await GetBusinessAggregate(business.Id);
        businessWithoutEmployee.Employees.Should().HaveCount(0);
    }

    [Fact]
    public async Task RequestWithoutIds_DeleteEmployeeFails()
    {
        await CreateUserAndSetJwtToken();
        var problemDetails = await DeleteAsyncAndEnsureBadRequest($"{EmployeeRoute}/{Guid.Empty}?businessId={Guid.Empty}");
        problemDetails?.Errors.Should().HaveCount(typeof(DeleteEmployeeRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_DeleteEmployee_ReturnBusinessNotFound()
    {
        var employeeUserId = await CreateUserAndSetJwtToken();
        var (_, business) = await CreateUserWithBusiness();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);
        var businessWithEmployee = await GetBusinessAggregate(business.Id);
        var employee = businessWithEmployee.Employees.First();

        var problemDetails = await DeleteAsyncAndEnsureNotFound($"{EmployeeRoute}/{employee.Id}?businessId={Guid.NewGuid()}");

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidEmployeeId_DeleteEmployee_ReturnEmployeeNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var problemDetails = await DeleteAsyncAndEnsureNotFound($"{EmployeeRoute}/{Guid.NewGuid()}?businessId={business.Id}");

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(EmployeeErrors.NotFound.Code);
    }
}
