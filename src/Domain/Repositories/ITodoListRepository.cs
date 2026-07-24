namespace Domain.Repositories;

public interface ITodoListRepository
{
    public Task DeleteAsync(TodoList entity, CancellationToken cancellationToken);
    public Task<TodoList> GetAsync(Guid id);
    public IQueryable<TodoList> GetAll();
    public Task InsertAsync(TodoList entity, CancellationToken cancellationToken);
    public Task UpdateAsync(TodoList entity, CancellationToken cancellationToken);
}
