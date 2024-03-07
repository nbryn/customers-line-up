namespace CLup.API.Contracts.Users.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(request => request.Email).EmailAddress();
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Zip).NotEmpty();
        RuleFor(request => request.Street).NotEmpty();
        RuleFor(request => request.City).NotEmpty();
        RuleFor(request => request.Latitude).NotEmpty();
        RuleFor(request => request.Longitude).NotEmpty();
    }
}
