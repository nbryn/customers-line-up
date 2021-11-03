using System.Collections.Generic;

using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Messages
{
    public class MessageData : ValueObject
    {
        public string Title { get; private set; }

        public string Content { get; private set; }

        public MessageData(
            string title,
            string content)
            : base()
        {
            Title = title;
            Content = content;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Content;
        }
    }
}