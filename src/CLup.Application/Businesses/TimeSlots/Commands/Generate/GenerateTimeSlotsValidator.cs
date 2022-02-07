using FluentValidation;

namespace CLup.Application.Businesses.TimeSlots.Commands.Generate
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