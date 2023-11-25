using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commands.ExpenseCommands
{
    public class CreateExpenseCommand : ExpenseCommand, IRequest<int>
    {
        public CreateExpenseCommand(int userId, string name, int expenseTypeid,
            int categoryId, int installments, double installmentAmmount, DateTime installmentDueDate) 
        {
            UserId = userId;
            Name = name;
            ExpenseTypeId = expenseTypeid;
            CategoryId = categoryId;
            Installments = installments;
            ExpenseInstallmentAmmount = installmentAmmount;
            ExpenseInstallmentDueDate = installmentDueDate;
        }
    }
}
