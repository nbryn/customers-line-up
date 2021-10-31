using MediatR;

using CLup.Features.Shared;

namespace CLup.Features.Users.Commands
{
    public class RegisterCommand : IRequest<Result<UserDTO>>
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}

