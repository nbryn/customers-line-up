using MediatR;

using CLup.Features.Common;

namespace CLup.Features.Users.Commands
{
    public class UpdateUserInfoCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }
        public string Address { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}

