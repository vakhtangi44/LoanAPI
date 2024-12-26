namespace Domain.Exceptions
{
    public class UserNotFoundException(int userId)
        : BaseException($"User with ID {userId} was not found.", "USER_NOT_FOUND");
}
