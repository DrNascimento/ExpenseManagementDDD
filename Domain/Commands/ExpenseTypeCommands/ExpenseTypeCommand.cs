namespace Domain.Commands.ExpenseTypeCommands
{
    public class ExpenseTypeCommand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsFixed { get; set; }
    }
}