using CLup.API.Contracts.Messages.MarkBusinessMessageAsDeleted;
using CLup.API.Contracts.Messages.MarkUserMessageAsDeleted;
using CLup.API.Contracts.Messages.SendBusinessMessage;
using CLup.API.Contracts.Messages.SendUserMessage;
using CLup.Application.Shared;
using CLup.Domain.Businesses;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;

namespace tests.CLup.IntegrationTests.Tests;

public sealed class MessageControllerTests : IntegrationTestsBase
{
    public MessageControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_SendUserMessageSucceeds()
    {
        const string email = "test@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var userWithMessage = await GetUser();
        var businessWithMessage = await GetBusiness(business);

        userWithMessage.SentMessages.Should().HaveCount(1);
        userWithMessage.SentMessages.First().ReceiverId.Should().Be(business.Id);
        userWithMessage.SentMessages.First().Title.Should().Be(sendUserMessageRequest.Title);

        businessWithMessage.ReceivedMessages.Should().HaveCount(1);
        businessWithMessage.ReceivedMessages.First().SenderId.Should().Be(userId);
        businessWithMessage.ReceivedMessages.First().Content.Should().Be(sendUserMessageRequest.Content);
    }

    [Fact]
    public async Task EmptyRequest_SendUserMessageFails()
    {
        const string email = "test1@test.com";
        await CreateUserWithBusiness(email);

        var sendUserMessageRequest = new SendUserMessageRequest();
        var problemDetails =
            await PostAsyncAndEnsureBadRequest<SendUserMessageRequest, ProblemDetails>($"{MessageRoute}/user",
                sendUserMessageRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(SendUserMessageRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_SendUserMessage_ReturnsBusinessNotFound()
    {
        const string email = "test2@test.com";
        await CreateUserAndSetJwtToken(email);

        var sendUserMessageRequest = new SendUserMessageRequest(Guid.NewGuid(), "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound<SendUserMessageRequest, ProblemDetails>(
            $"{MessageRoute}/user",
            sendUserMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_SendBusinessMessageSucceeds()
    {
        const string email = "test3@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, userId, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/business", sendBusinessMessageRequest);

        var userWithMessage = await GetUser();
        var businessWithMessage = await GetBusiness(business);

        userWithMessage.ReceivedMessages.Should().HaveCount(1);
        userWithMessage.ReceivedMessages.First().SenderId.Should().Be(business.Id);
        userWithMessage.ReceivedMessages.First().Title.Should().Be(sendBusinessMessageRequest.Title);

        businessWithMessage.SentMessages.Should().HaveCount(1);
        businessWithMessage.SentMessages.First().ReceiverId.Should().Be(userId);
        businessWithMessage.SentMessages.First().Content.Should().Be(sendBusinessMessageRequest.Content);
    }

    [Fact]
    public async Task EmptyRequest_SendBusinessMessageFails()
    {
        const string email = "test4@test.com";
        await CreateUserWithBusiness(email);

        var sendBusinessMessageRequest = new SendBusinessMessageRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest<SendBusinessMessageRequest, ProblemDetails>(
            $"{MessageRoute}/business",
            sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(SendBusinessMessageRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_SendBusinessMessage_ReturnsBusinessNotFound()
    {
        const string email = "test5@test.com";
        var userId = await CreateUserWithBusiness(email);

        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(Guid.NewGuid(), userId, "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound<SendBusinessMessageRequest, ProblemDetails>(
            $"{MessageRoute}/business",
            sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidUserId_SendBusinessMessage_ReturnsUserNotFound()
    {
        const string email = "test6@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, Guid.NewGuid(), "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound<SendBusinessMessageRequest, ProblemDetails>(
            $"{MessageRoute}/business",
            sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(UserErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_MarkUserMessageAsDeleted_Succeeds()
    {
        const string email = "test7@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var user = await GetUser();
        var message = user.SentMessages.First();

        var markMessageAsDeletedRequest = new MarkUserMessageAsDeletedRequest(message.Id, business.Id,true);
        await PutAsyncAndEnsureSuccess($"{MessageRoute}/user", markMessageAsDeletedRequest);

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesByOwner(userId)).First();

        updatedUser.SentMessages.Should().BeEmpty();
        updatedBusiness.ReceivedMessages.Should().HaveCount(1);
    }

    [Fact]
    public async Task InvalidRequest_MarkUserMessageAsDeleted_Fails()
    {
        const string email = "test8@test.com";
        await CreateUserAndSetJwtToken(email);

        var markMessageAsDeletedRequest = new MarkUserMessageAsDeletedRequest(Guid.Empty, Guid.Empty, null);
        var problemDetails =
            await PutAsyncAndEnsureBadRequest<MarkUserMessageAsDeletedRequest, ProblemDetails>($"{MessageRoute}/user",
                markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(MarkUserMessageAsDeletedRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidMessageId_MarkUserMessageAsDeleted_ReturnsMessageNotFound()
    {
        const string email = "test9@test.com";
        await CreateUserAndSetJwtToken(email);

        var markMessageAsDeletedRequest = new MarkUserMessageAsDeletedRequest(Guid.NewGuid(), Guid.NewGuid(), true);
        var problemDetails =
            await PutAsyncAndEnsureNotFound<MarkUserMessageAsDeletedRequest, ProblemDetails>($"{MessageRoute}/user",
                markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(MessageErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_MarkBusinessMessageAsDeleted_Succeeds()
    {
        const string email = "test10@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, userId, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/business", sendBusinessMessageRequest);

        var updatedBusiness = await GetBusiness(business);
        var message = updatedBusiness.SentMessages.First();

        var markMessageAsDeletedRequest = new MarkBusinessMessageAsDeletedRequest(business.Id, userId, message.Id, true);
        await PutAsyncAndEnsureSuccess($"{MessageRoute}/business", markMessageAsDeletedRequest);

        var updatedUser = await GetUser();
        var businessWithMessage = (await GetBusinessesByOwner(userId)).First();

        updatedUser.ReceivedMessages.Should().HaveCount(1);
        businessWithMessage.SentMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task InvalidRequest_MarkBusinessMessageAsDeleted_Fails()
    {
        const string email = "test11@test.com";
        await CreateUserAndSetJwtToken(email);

        var markMessageAsDeletedRequest = new MarkBusinessMessageAsDeletedRequest( Guid.Empty, Guid.Empty, Guid.Empty, null);
        var problemDetails =
            await PutAsyncAndEnsureBadRequest<MarkBusinessMessageAsDeletedRequest, ProblemDetails>(
                $"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(MarkBusinessMessageAsDeletedRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_MarkBusinessMessageAsDeleted_ReturnsBusinessNotFound()
    {
        const string email = "test12@test.com";
        await CreateUserAndSetJwtToken(email);

        var markMessageAsDeletedRequest = new MarkBusinessMessageAsDeletedRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),true);
        var problemDetails =
            await PutAsyncAndEnsureNotFound<MarkBusinessMessageAsDeletedRequest, ProblemDetails>(
                $"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidMessageId_MarkBusinessMessageAsDeleted_ReturnsMessageNotFound()
    {
        const string email = "test13@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var markMessageAsDeletedRequest = new MarkBusinessMessageAsDeletedRequest(business.Id, Guid.NewGuid(), Guid.NewGuid(), true);
        var problemDetails =
            await PutAsyncAndEnsureNotFound<MarkBusinessMessageAsDeletedRequest, ProblemDetails>(
                $"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(MessageErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequests_MessageDeletedByReceiverAndSender_MessageDeleted()
    {
        const string email = "test14@test.com";
        var userId = await CreateUserWithBusiness(email);
        var business = (await GetBusinessesByOwner(userId)).First();

        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var user = await GetUser();
        var message = user.SentMessages.First();

        var markMessageAsDeletedRequest1 = new MarkUserMessageAsDeletedRequest(message.Id, business.Id, true);
        await PutAsyncAndEnsureSuccess($"{MessageRoute}/user", markMessageAsDeletedRequest1);

        var markMessageAsDeletedRequest2 = new MarkUserMessageAsDeletedRequest(message.Id, business.Id, false);
        await PutAsyncAndEnsureSuccess($"{MessageRoute}/user", markMessageAsDeletedRequest2);

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesByOwner(userId)).First();

        updatedUser.SentMessages.Should().BeEmpty();
        updatedBusiness.ReceivedMessages.Should().BeEmpty();
    }
}
