using FluentValidation;

namespace CLup.Application.Commands.Business.TimeSlot.Generate
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