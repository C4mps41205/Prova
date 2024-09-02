namespace Infra.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a user with the same name already exists.
/// </summary>
/// <remarks>
/// This exception is typically thrown when attempting to create a new user with a name that is already in use.
/// </remarks>
/// <example>
/// try
/// {
///     // Attempt to create a new user
///     CreateUser(newUser);
/// }
/// catch (UserAlreadyExistsException ex)
/// {
///     // Handle the exception, e.g. display an error message to the user
///     Console.WriteLine(ex.Message);
/// }
/// </example>
public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("A user with the same Name already exists.")
    {
    }

    public UserAlreadyExistsException(string message) : base(message)
    {
    }

    public UserAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}