namespace Domain.Commands.ExpenseTypeCommands;

public class ExpenseTypeCommand
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public bool IsFixed { get; set; }
}