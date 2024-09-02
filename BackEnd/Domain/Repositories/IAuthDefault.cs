namespace Application.Repositories;

public interface IAuthDefault<T>
{
    public object Authenticate(T login);
}