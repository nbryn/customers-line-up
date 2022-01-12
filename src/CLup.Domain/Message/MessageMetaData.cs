using System.Collections.Generic;
using CLup.Domain.Shared.ValueObjects;

namespace CLup.Domain.Message
{
    public class MessageMetadata : ValueObject
    {

        public bool DeletedBySender { get; private set; }

        public bool DeletedByReceiver { get; private set; }

        public MessageMetadata(
            bool deletedBySender = false,
            bool deletedByReceiver = false)
            : base()
        {
            DeletedBySender = deletedBySender;
            DeletedByReceiver = deletedByReceiver;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DeletedBySender;
            yield return DeletedByReceiver;
        }
    }
}