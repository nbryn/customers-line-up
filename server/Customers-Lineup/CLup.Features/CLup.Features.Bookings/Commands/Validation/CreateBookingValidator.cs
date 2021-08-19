using FluentValidation;

namespace CLup.Features.Bookings.Commands.Validation
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(b => b.UserEmail).NotNull();
            RuleFor(b => b.TimeSlotId).NotNull();           
        }
    }
}