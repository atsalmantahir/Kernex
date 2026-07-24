using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Repositories;

namespace Application.Feature.TodoList.Queries.List;

public record TodoListsQuery() : PaginatedQuery, IRequest<PaginatedList<TodoListVM>>;
public class TodoListsQueryHandler : IRequestHandler<TodoListsQuery, PaginatedList<TodoListVM>>
{
    private readonly ITodoListRepository repository;

    public TodoListsQueryHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<TodoListVM>> Handle(TodoListsQuery request, CancellationToken cancellationToken)
    {
        var todoLists = this.repository.GetAll();
        var response = todoLists.ToDto();
        return await PaginatedList<TodoListVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}
