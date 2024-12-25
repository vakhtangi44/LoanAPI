namespace Application.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
