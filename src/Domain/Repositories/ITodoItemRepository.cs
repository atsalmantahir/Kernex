namespace Domain.Repositories;

public interface ITodoItemRepository
{
    public Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken);
    public Task<TodoItem> GetAsync(Guid id);
    public IQueryable<TodoItem> GetAll();
    public Task InsertAsync(TodoItem entity, CancellationToken cancellationToken);
    public Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken);
}
