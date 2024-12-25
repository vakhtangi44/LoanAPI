namespace Domain.Entities
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public DateTime Timestamp { get; set; }
        public string? RequestPath { get; set; }
        public string? UserIdentifier { get; set; }
    }
}
