using Application.Common.Mappings;
using Domain.Repositories;

namespace Application.Feature.TodoList.Queries.Get;

public record TodoListQuery(Guid id) : IRequest<TodoListVM>;

public class TodoListQueryHandler : IRequestHandler<TodoListQuery, TodoListVM> 
{
    private readonly ITodoListRepository repository;

    public TodoListQueryHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async Task<TodoListVM> Handle(TodoListQuery request, CancellationToken cancellationToken)
    {
        var todoList = await this.repository.GetAsync(request.id);
        return todoList.ToQuerytodoItemDto();
    }
}
