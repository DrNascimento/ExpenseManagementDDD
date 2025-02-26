using Domain.Commands.ExpenseCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.ExpenseCommandHandlers;

public class CreateExpenseCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IExpenseInstallmentRepository _expenseInstallmentRepository;

    public CreateExpenseCommandHandler(IUnitOfWork unitOfWork,
        IExpenseRepository expenseRepository,
        IExpenseInstallmentRepository expenseInstallmentRepository) : base(unitOfWork)
    {
        _expenseRepository = expenseRepository;
        _expenseInstallmentRepository = expenseInstallmentRepository;
    }

    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var expense = new Expense
            {
                UserId = request.UserId,
                Name = request.Name,
                ExpenseTypeId = request.ExpenseTypeId,
                CategoryId = request.CategoryId,
                Installments = request.Installments
            };

            await _uow.BeginTransactionAsync(cancellationToken);

            _expenseRepository.Add(expense);
            _expenseRepository.SaveChanges();

            await SaveExpenseInstallmentsAsync(expense.Id, request);

            await _uow.CommitTransactionAsync(cancellationToken);

            return expense.Id;
        }
        catch
        {
            await _uow.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    private async Task SaveExpenseInstallmentsAsync(Guid expenseId, CreateExpenseCommand createExpenseCommand)
    {
        List<ExpenseInstallment> expenseInstallments = [];

        for (int i = 0; i < createExpenseCommand.Installments; i++)
            expenseInstallments.Add(new ExpenseInstallment
            {
                ExpenseId = expenseId,
                InstallmentNumber = i + 1,
                Amount = createExpenseCommand.ExpenseInstallmentAmmount,
                DueDate = createExpenseCommand.ExpenseInstallmentDueDate.AddMonths(i),
                IsPaid = false,
            });

        await _expenseInstallmentRepository.Add(expenseInstallments);

        _expenseInstallmentRepository.SaveChanges();
    }
}
