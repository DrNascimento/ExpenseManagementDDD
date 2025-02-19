using MediatR;

namespace Domain.Commands.ExpenseCommands;

public class UpdateExpenseCommand : ExpenseCommand, IRequest<Unit>
{
    public UpdateExpenseCommand(Guid id, string name, Guid categoryId) 
    { 
        Id = id;
        Name = name;
        CategoryId = categoryId;
    }
}
