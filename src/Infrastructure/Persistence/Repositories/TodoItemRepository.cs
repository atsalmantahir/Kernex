using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly IApplicationDbContext dbContext;

    public TodoItemRepository(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task DeleteAsync(TodoItem entity, CancellationToken cancellationToken)
    {
        entity.IsDeleted = true;
        this.dbContext.TodoItems.Update(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TodoItem> GetAsync(Guid id)
    {
        return await this
            .dbContext
            .TodoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id && x.IsDeleted == false);
    }

    public IQueryable<TodoItem> GetAll()
    {
        return this.dbContext.TodoItems.Where(x => x.IsDeleted == false)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.LastModifiedAt);
    }

    public async Task InsertAsync(TodoItem entity, CancellationToken cancellationToken)
    {
        await this.dbContext.TodoItems.AddAsync(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TodoItem entity, CancellationToken cancellationToken)
    {
        this.dbContext.TodoItems.Update(entity);
        await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}
