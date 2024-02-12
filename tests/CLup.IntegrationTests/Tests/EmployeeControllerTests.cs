using CLup.API.Contracts.Employees.CreateEmployee;
using CLup.API.Contracts.Employees.DeleteEmployee;
using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.Employees;
using CLup.Domain.Users;

namespace tests.CLup.IntegrationTests.Tests;

public class EmployeeControllerTests : IntegrationTestsBase
{
    public EmployeeControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_CreateEmployeeSucceeds()
    {
        const string employeeEmail = "test@test.com";
        var employeeUserId = await CreateUserAndSetJwtToken(employeeEmail);

        const string ownerEmail = "test1@test.com";
        var ownerUserId = await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesByOwner(ownerUserId)).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);

        var updatedBusiness = await GetBusiness(business);
        updatedBusiness.Employees.Should().HaveCount(1);
        updatedBusiness.Employees.First().UserId.Should().Be(employeeUserId);
    }

    [Fact]
    public async Task RequestWhereUserIsOwner_CreateEmployee_ReturnsOwnerCantBeEmployee()
    {
        const string ownerEmail = "test2@test.com";
        var userId = await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesByOwner(userId)).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, userId);
        var problemDetails = await PostAsyncAndEnsureBadRequest<CreateEmployeeRequest, ProblemDetails>(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(EmployeeErrors.OwnerCannotBeEmployee.Code);
    }

    [Fact]
    public async Task RequestWithoutIds_CreateEmployeeFails()
    {
        const string ownerEmail = "test3@test.com";
        await CreateUserAndSetJwtToken(ownerEmail);

        var createEmployeeRequest = new CreateEmployeeRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest<CreateEmployeeRequest, ProblemDetails>(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(CreateEmployeeRequest).GetProperties().Length - 1);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_CreateEmployee_ReturnsBusinessNotFound()
    {
        const string employeeEmail = "test4@test.com";
        var employeeUserId = await CreateUserAndSetJwtToken(employeeEmail);

        const string ownerEmail = "test5@test.com";
        await CreateUserAndSetJwtToken(ownerEmail);

        var createEmployeeRequest = new CreateEmployeeRequest(Guid.NewGuid(), employeeUserId);
        var problemDetails = await PostAsyncAndEnsureNotFound<CreateEmployeeRequest, ProblemDetails>(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidUserId_CreateEmployee_ReturnsUserNotFound()
    {
        const string ownerEmail = "test6@test.com";
        var userId = await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesByOwner(userId)).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, Guid.NewGuid());
        var problemDetails = await PostAsyncAndEnsureNotFound<CreateEmployeeRequest, ProblemDetails>(EmployeeRoute, createEmployeeRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(UserErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_DeleteEmployeeSucceeds()
    {
        const string employeeEmail = "test7@test.com";
        var employeeUserId = await CreateUserAndSetJwtToken(employeeEmail);

        const string ownerEmail = "test8@test.com";
        var ownerUserId = await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesByOwner(ownerUserId)).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);
        var businessWithEmployee = await GetBusiness(business);
        var employee = businessWithEmployee.Employees.First();

        await DeleteAsyncAndEnsureSuccess($"{EmployeeRoute}/{employee.Id}?businessId={business.Id}");

        var businessWithoutEmployee = await GetBusiness(business);
        businessWithoutEmployee.Employees.Should().HaveCount(0);
    }

    [Fact]
    public async Task RequestWithoutIds_DeleteEmployeeFails()
    {
        const string ownerEmail = "test9@test.com";
        await CreateUserAndSetJwtToken(ownerEmail);

        var problemDetails = await DeleteAsyncAndEnsureBadRequest<ProblemDetails>($"{EmployeeRoute}/{Guid.Empty}?businessId={Guid.Empty}");
        problemDetails?.Errors.Should().HaveCount(typeof(DeleteEmployeeRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_DeleteEmployee_ReturnBusinessNotFound()
    {
        const string employeeEmail = "test10@test.com";
        var employeeUserId = await CreateUserAndSetJwtToken(employeeEmail);

        const string ownerEmail = "test11@test.com";
        var ownerUserId = await CreateUserWithBusiness(ownerEmail);
        var business = (await GetBusinessesByOwner(ownerUserId)).First();

        var createEmployeeRequest = new CreateEmployeeRequest(business.Id, employeeUserId);
        await PostAsyncAndEnsureSuccess(EmployeeRoute, createEmployeeRequest);
        var businessWithEmployee = await GetBusiness(business);
        var employee = businessWithEmployee.Employees.First();

        var problemDetails = await DeleteAsyncAndEnsureNotFound<ProblemDetails>($"{EmployeeRoute}/{employee.Id}?businessId={Guid.NewGuid()}");

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidEmployeeId_DeleteEmployee_ReturnEmployeeNotFound()
    {
        const string userEmail = "test12@test.com";
        var ownerUserId = await CreateUserWithBusiness(userEmail);
        var business = (await GetBusinessesByOwner(ownerUserId)).First();

        var problemDetails = await DeleteAsyncAndEnsureNotFound<ProblemDetails>($"{EmployeeRoute}/{Guid.NewGuid()}?businessId={business.Id}");

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(EmployeeErrors.NotFound.Code);
    }
}
