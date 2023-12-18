using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;
using FluentValidation;

namespace CLup.Application.Messages
{
    public class MessageMetadataValidator : AbstractValidator<MessageMetadata>
    {
        public MessageMetadataValidator()
        {
        }
    }
}