namespace Domain.Exceptions
{
    public class UserNotFoundException : BaseException
    {
        public UserNotFoundException(Guid userId)
            : base($"User with ID {userId} was not found.", "USER_NOT_FOUND")
        {
        }
    }
}
