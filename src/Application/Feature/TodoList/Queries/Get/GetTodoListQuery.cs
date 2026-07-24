using Application.Common.Mappings;
using Domain.Repositories;

namespace Application.Feature.TodoList.Queries.Get;

public record GetTodoListQuery(Guid id) : IRequest<TodoListVM>;

public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, TodoListVM> 
{
    private readonly ITodoListRepository repository;

    public GetTodoListQueryHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async Task<TodoListVM> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        var todoList = await this.repository.GetAsync(request.id);
        return todoList.ToQuerytodoItemDto();
    }
}
