using CLup.Domain.Messages;
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