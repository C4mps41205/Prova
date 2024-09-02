using Microsoft.EntityFrameworkCore;

namespace Application.Repositories;

public interface IContextFactory
{
    DbContext CreateDbFactory();
}