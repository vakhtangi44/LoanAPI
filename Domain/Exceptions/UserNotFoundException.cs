namespace Domain.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(int userId)
            : base($"User with ID {userId} was not found.", "USER_NOT_FOUND")
        {
        }
    }
}
