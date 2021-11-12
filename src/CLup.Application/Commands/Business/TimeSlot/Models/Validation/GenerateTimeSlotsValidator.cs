using FluentValidation;

namespace CLup.Application.Commands.Business.TimeSlot.Models.Validation
{
    public class GenerateTimeSlotsValidator : AbstractValidator<GenerateTimeSlotsCommand>
    {
        public GenerateTimeSlotsValidator()
        {
            RuleFor(x => x.BusinessId).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
        }
    }
}