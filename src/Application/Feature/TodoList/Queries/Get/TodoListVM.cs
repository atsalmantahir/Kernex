using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.TodoList.Queries.Get;

public class TodoListVM
{
    public Guid ID { get; set; }

    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;
}
