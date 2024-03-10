using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Messages.ValueObjects;

public sealed class MessageData : ValueObject
{
    public string Title { get; private set; }

    public string Content { get; private set; }

    public MessageData(string title, string content)
    {
        Guard.Against.NullOrWhiteSpace(title);
        Guard.Against.NullOrWhiteSpace(content);

        Title = title;
        Content = content;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return Content;
    }
}
