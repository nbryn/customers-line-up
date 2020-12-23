namespace Logic.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrivateEmail { get; set; }
        public string CompanyEmail { get; set; }
        public string Business { get; set; }
        public int BusinessId { get; set; }
        public string EmployedSince { get; set; }
    }
}