namespace Domain.Entities
{
    public class Accountant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } = "Accountant";

        public Accountant() { }

        public Accountant(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
