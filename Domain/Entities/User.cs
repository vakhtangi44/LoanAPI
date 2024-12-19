namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Age { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; } = false;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
