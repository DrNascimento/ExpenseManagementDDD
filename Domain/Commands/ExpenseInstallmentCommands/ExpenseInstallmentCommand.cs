namespace Domain.Commands.ExpenseInstallmentCommands;

public class ExpenseInstallmentCommand
{
    public Guid Id { get; protected set; }

    public double Amount { get; protected set; }

    public DateTime DueDate { get; protected set; }

    public bool IsPaid { get; protected set; }

    public Guid UserId { get; protected set; }

}
