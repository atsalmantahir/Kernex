using Application.Feature.TodoList.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kernex.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly IMediator mediator;

    public TodosController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IResult> GetTodosAsync([FromQuery] GetTodoListQuery query) 
    {
        var response = await mediator.Send(query);
        return Results.Ok(response);
    }
}
