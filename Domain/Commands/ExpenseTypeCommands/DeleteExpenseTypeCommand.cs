using MediatR;

namespace Domain.Commands.ExpenseTypeCommands
{
    public class DeleteExpenseTypeCommand : ExpenseTypeCommand, IRequest<Unit>
    {
        public DeleteExpenseTypeCommand(int id)
        {
            Id = id;
        }
    }
}