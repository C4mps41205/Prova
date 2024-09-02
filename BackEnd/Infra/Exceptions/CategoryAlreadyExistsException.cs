namespace Infra.Exceptions;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException() : base("A categody with the same Name already exists.")
    {
    }

    public CategoryAlreadyExistsException(string message) : base(message)
    {
    }

    public CategoryAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}