namespace Application.DTOs
{
    public class ErrorLogDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
