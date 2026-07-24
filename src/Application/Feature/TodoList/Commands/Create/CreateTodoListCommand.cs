using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Repositories;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Application.Feature.TodoList.Commands.Create;


public record CreateTodoListCommand : IRequest<CreateTodoListCommand>
{
    //public EntityIdentifier Organisation { get; set; }

    [Required]
    public string Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;
}


public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, CreateTodoListCommand>
{
    private readonly ITodoListRepository repository;

    public CreateTodoListCommandHandler(ITodoListRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateTodoListCommand> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToCreateTodoListEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}
