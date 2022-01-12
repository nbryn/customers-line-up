using CLup.Domain.Message;
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