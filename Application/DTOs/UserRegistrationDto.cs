namespace Application.DTOs
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string Password { get; set; }
    }
}
