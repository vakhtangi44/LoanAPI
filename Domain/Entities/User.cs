namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Loan>? Loans { get; set; }
    }
}