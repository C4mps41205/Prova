using Domain.Entities;

namespace Application.Repositories;

public interface ICrudDefault<D, T>
{
   public Task<D> Create(D entity);
   public Task<D> Update(TaskToDo entity);
   public Task<D> Delete(TaskToDo entity);
   public Task<D> List(int id);
   Task<List<D>> GetAll(int id, int? filter);
}