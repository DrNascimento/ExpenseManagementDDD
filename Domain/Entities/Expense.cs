using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Expense : EntityBase
{

    public Expense() { }

    public Expense(Guid id, DateTime created, DateTime? updated, bool isDeleted, User user, Guid userId, string name, ExpenseType expenseType, Guid expenseTypeId, Category category, Guid categoryId, int installments, ICollection<ExpenseInstallment> expenseInstallments)
    {
        Id = id;
        Created = created;
        Updated = updated;
        IsDeleted = isDeleted;
        User = user;
        UserId = userId;
        Name = name;
        ExpenseType = expenseType;
        ExpenseTypeId = expenseTypeId;
        Category = category;
        CategoryId = categoryId;
        Installments = installments;
        ExpenseInstallments = expenseInstallments;
    }

    public User User { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Name { get; set; }

    public ExpenseType ExpenseType { get; set; }

    [Required]
    public Guid ExpenseTypeId { get;set; }

    public Category Category { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required]
    public int Installments { get; set; }

    public ICollection<ExpenseInstallment> ExpenseInstallments { get; set; } = new List<ExpenseInstallment>();
}
