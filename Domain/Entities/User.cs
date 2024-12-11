namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string PasswordHash { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public User() { }

        public User(string firstName, string lastName, string userName, string email, int age, decimal monthlyIncome, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Age = age;
            MonthlyIncome = monthlyIncome;
            PasswordHash = passwordHash;
        }
    }
}
