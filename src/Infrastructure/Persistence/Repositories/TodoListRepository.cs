using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly IApplicationDbContext dbContext;

    public TodoListRepository(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task DeleteAsync(TodoList entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.dbContext.TodoLists.Update(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TodoList> GetAsync(Guid id)
    {
        return await this
            .dbContext
            .TodoLists
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id && x.IsDeleted == false);
    }

    public IQueryable<TodoList> GetAll()
    {
        return this.dbContext.TodoLists.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(TodoList entity, CancellationToken cancellationToken)
    {
        await this.dbContext.TodoLists.AddAsync(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TodoList entity, CancellationToken cancellationToken)
    {
        this.dbContext.TodoLists.Update(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
