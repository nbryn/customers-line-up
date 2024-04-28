using CLup.API.Messages.Contracts.MarkMessageAsDeletedForBusiness;
using CLup.API.Messages.Contracts.MarkMessageAsDeletedForUser;
using CLup.API.Messages.Contracts.SendBusinessMessage;
using CLup.API.Messages.Contracts.SendUserMessage;
using CLup.Application.Businesses;
using CLup.Domain.Businesses;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Users;

#pragma warning disable CA1707
namespace tests.CLup.IntegrationTests.Tests;

public sealed class MessageControllerTests : IntegrationTestsBase
{
    public MessageControllerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ValidRequest_SendUserMessageSucceeds()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var userWithMessage = await GetUser();
        var businessWithMessage = await GetBusinessAggregate(business.Id);

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
        await CreateUserWithBusiness();
        var sendUserMessageRequest = new SendUserMessageRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest($"{MessageRoute}/user", sendUserMessageRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(SendUserMessageRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_SendUserMessage_ReturnsBusinessNotFound()
    {
        await CreateUserAndSetJwtToken();
        var sendUserMessageRequest = new SendUserMessageRequest(Guid.NewGuid(), "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound($"{MessageRoute}/user", sendUserMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_SendBusinessMessageSucceeds()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, userId, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/business", sendBusinessMessageRequest);

        var userWithMessage = await GetUser();
        var businessWithMessage = await GetBusinessAggregate(business.Id);

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
        await CreateUserWithBusiness();
        var sendBusinessMessageRequest = new SendBusinessMessageRequest();
        var problemDetails = await PostAsyncAndEnsureBadRequest($"{MessageRoute}/business", sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(SendBusinessMessageRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_SendBusinessMessage_ReturnsBusinessNotFound()
    {
        var (userId, _) = await CreateUserWithBusiness();
        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(Guid.NewGuid(), userId, "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound($"{MessageRoute}/business", sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidUserId_SendBusinessMessage_ReturnsUserNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, Guid.NewGuid(), "Temp", "Hello", MessageType.Enquiry);
        var problemDetails = await PostAsyncAndEnsureNotFound($"{MessageRoute}/business", sendBusinessMessageRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(UserErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_MarkUserMessageAsDeleted_Succeeds()
    {
       var (_, business) = await CreateUserWithBusiness();
        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var user = await GetUser();
        var message = user.SentMessages.First();

        var markMessageAsDeletedRequest = new MarkMessageAsDeletedForUserRequest(message.Id, false);
        await PatchAsyncAndEnsureSuccess($"{MessageRoute}/user", markMessageAsDeletedRequest);

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesForCurrentUser()).First();

        updatedUser.SentMessages.Should().BeEmpty();
        updatedBusiness.ReceivedMessages.Should().HaveCount(1);
    }

    [Fact]
    public async Task InvalidRequest_MarkUserMessageAsDeleted_Fails()
    {
        await CreateUserAndSetJwtToken();
        var markMessageAsDeletedRequest = new MarkMessageAsDeletedForUserRequest(Guid.Empty, null);
        var problemDetails = await PatchAsyncAndEnsureBadRequest($"{MessageRoute}/user", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(typeof(MarkMessageAsDeletedForUserRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidMessageId_MarkUserMessageAsDeleted_ReturnsMessageNotFound()
    {
        await CreateUserAndSetJwtToken();
        var markMessageAsDeletedRequest = new MarkMessageAsDeletedForUserRequest(Guid.NewGuid(), false);
        var problemDetails = await PatchAsyncAndEnsureNotFound($"{MessageRoute}/user", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(MessageErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequest_MarkBusinessMessageAsDeleted_Succeeds()
    {
        var (userId, business) = await CreateUserWithBusiness();
        var sendBusinessMessageRequest =
            new SendBusinessMessageRequest(business.Id, userId, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/business", sendBusinessMessageRequest);

        var updatedBusiness = await GetBusinessAggregate(business.Id);
        var message = updatedBusiness.SentMessages.First();

        var markMessageAsDeletedRequest = new MarkMessageAsDeletedForBusinessRequest(business.Id, message.Id, false);
        await PatchAsyncAndEnsureSuccess($"{MessageRoute}/business", markMessageAsDeletedRequest);

        var updatedUser = await GetUser();
        var businessWithMessage = (await GetBusinessesForCurrentUser()).First();

        updatedUser.ReceivedMessages.Should().HaveCount(1);
        businessWithMessage.SentMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task InvalidRequest_MarkBusinessMessageAsDeleted_Fails()
    {
        await CreateUserAndSetJwtToken();
        var markMessageAsDeletedRequest = new MarkMessageAsDeletedForBusinessRequest(Guid.Empty, Guid.Empty, null);
        var problemDetails = await PatchAsyncAndEnsureBadRequest($"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors
            .Should()
            .HaveCount(typeof(MarkMessageAsDeletedForBusinessRequest).GetProperties().Length);
    }

    [Fact]
    public async Task RequestWithInvalidBusinessId_MarkBusinessMessageAsDeleted_ReturnsBusinessNotFound()
    {
        await CreateUserAndSetJwtToken();
        var markMessageAsDeletedRequest =
            new MarkMessageAsDeletedForBusinessRequest(Guid.NewGuid(), Guid.NewGuid(), false);
        var problemDetails = await PatchAsyncAndEnsureNotFound($"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(BusinessErrors.NotFound.Code);
    }

    [Fact]
    public async Task RequestWithInvalidMessageId_MarkBusinessMessageAsDeleted_ReturnsMessageNotFound()
    {
        var (_, business) = await CreateUserWithBusiness();
        var markMessageAsDeletedRequest =
            new MarkMessageAsDeletedForBusinessRequest(business.Id, Guid.NewGuid(), false);
        var problemDetails = await PatchAsyncAndEnsureNotFound($"{MessageRoute}/business", markMessageAsDeletedRequest);

        problemDetails?.Errors.Should().HaveCount(1);
        problemDetails?.Errors.First().Key.Should().Be(MessageErrors.NotFound.Code);
    }

    [Fact]
    public async Task ValidRequests_MessageDeletedByReceiverAndSender_MessageDeleted()
    {
        var (_, business) = await CreateUserWithBusiness();
        var sendUserMessageRequest = new SendUserMessageRequest(business.Id, "Temp", "Hello", MessageType.Enquiry);
        await PostAsyncAndEnsureSuccess($"{MessageRoute}/user", sendUserMessageRequest);

        var user = await GetUser();
        var message = user.SentMessages.First();

        var markMessageAsDeletedRequest1 = new MarkMessageAsDeletedForUserRequest(message.Id, false);
        await PatchAsyncAndEnsureSuccess($"{MessageRoute}/user", markMessageAsDeletedRequest1);

        var markMessageAsDeletedRequest2 = new MarkMessageAsDeletedForBusinessRequest(business.Id, message.Id, true);
        await PatchAsyncAndEnsureSuccess($"{MessageRoute}/business", markMessageAsDeletedRequest2);

        var updatedUser = await GetUser();
        var updatedBusiness = (await GetBusinessesForCurrentUser()).First();

        updatedUser.SentMessages.Should().BeEmpty();
        updatedBusiness.ReceivedMessages.Should().BeEmpty();
    }
}
