using CLup.Domain.Shared;

namespace CLup.Domain.Messages.Enums;

public static class MessageErrors
{
    public static Error NotFound() => new("Messages.NotFound", "The message was not found.");

    public static Error NoAccess() => new("Messages.NoAccess", "You can't edit this message.");

    public static Error InvalidSender() => new("Messages.InvalidSender", "Invalid sender.");

    public static Error InvalidReceiver() => new("Messages.InvalidReceiver", "Invalid receiver.");
}
