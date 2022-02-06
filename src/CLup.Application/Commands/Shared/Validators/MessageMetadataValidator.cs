using CLup.Domain.Messages;
using FluentValidation;

namespace CLup.Application.Commands.Shared.Validators
{
    public class MessageMetadataValidator : AbstractValidator<MessageMetadata>
    {
        public MessageMetadataValidator()
        {
        }
    }
}