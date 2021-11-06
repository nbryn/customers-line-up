namespace CLup.Application.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }      
        public string City { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}