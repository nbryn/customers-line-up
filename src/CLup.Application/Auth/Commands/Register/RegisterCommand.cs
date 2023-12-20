namespace CLup.Application.Auth.Commands.Register
{
    using CLup.Application.Shared;
    using MediatR;
    using Shared.Result;

    public class RegisterCommand : IRequest<Result<TokenResponse>>
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

