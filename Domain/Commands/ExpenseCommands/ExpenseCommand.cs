namespace Domain.Commands.ExpenseCommands;

public class ExpenseCommand
{       
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public Guid ExpenseTypeId { get; set; }

    public Guid CategoryId { get; set; }

    public int Installments { get; set; }

    public double ExpenseInstallmentAmmount { get; set; }

    public DateTime ExpenseInstallmentDueDate { get; set; }
}
