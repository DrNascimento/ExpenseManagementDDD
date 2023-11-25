using Domain.Commands.ExpenseCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.ExpenseCommandHandlers
{
    public class CreateExpenseCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateExpenseCommand, int>
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

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
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

                SaveExpenseInstallments(expense.Id, request);

                await _uow.CommitTransactionAsync(cancellationToken);

                return expense.Id;
            }
            catch (Exception)
            {
                await _uow.RollbackTransactionAsync(cancellationToken);
                throw;  
            }
        }

        private void SaveExpenseInstallments(int expenseId, CreateExpenseCommand createExpenseCommand)
        {
            for (var i = 0; i < createExpenseCommand.Installments;  i++)
            {
                var expenseInstallment = new ExpenseInstallment
                {
                    ExpenseId = expenseId,
                    InstallmentNumber = i + 1,
                    Amount = createExpenseCommand.ExpenseInstallmentAmmount,
                    DueDate = createExpenseCommand.ExpenseInstallmentDueDate.AddMonths(i),
                    IsPaid = false,
                };

                _expenseInstallmentRepository.Add(expenseInstallment);
            }

            _expenseInstallmentRepository.SaveChanges();
        }
    }
}
