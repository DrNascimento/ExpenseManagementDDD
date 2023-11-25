using MediatR;

namespace Domain.Commands.ExpenseTypeCommands
{
    public class CreateExpenseTypeCommand : ExpenseTypeCommand, IRequest<int>
    {
        public CreateExpenseTypeCommand(string name, bool isFixed)
        {
            Name = name;
            IsFixed = isFixed;
        }
    }
}