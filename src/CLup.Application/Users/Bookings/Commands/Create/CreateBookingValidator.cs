using FluentValidation;

namespace CLup.Application.Users.Bookings.Commands.Create
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(b => b.UserId).NotNull();
            RuleFor(b => b.TimeSlotId).NotNull(); 
            RuleFor(b => b.TimeSlotId).NotNull();           
        }
    }
}