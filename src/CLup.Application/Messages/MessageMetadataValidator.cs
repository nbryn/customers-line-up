using CLup.Domain.Messages;
using FluentValidation;

namespace CLup.Application.Shared.Messages
{
    public class MessageMetadataValidator : AbstractValidator<MessageMetadata>
    {
        public MessageMetadataValidator()
        {
        }
    }
}