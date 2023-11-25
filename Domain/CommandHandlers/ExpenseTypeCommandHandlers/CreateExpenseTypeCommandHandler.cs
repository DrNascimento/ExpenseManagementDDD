using Domain.Commands.ExpenseTypeCommands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.ExpenseTypeCommandHandlers
{
    public class CreateExpenseTypeCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateExpenseTypeCommand, int>
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public CreateExpenseTypeCommandHandler(IUnitOfWork unitOfWork,
            IExpenseTypeRepository expenseTypeRepository) 
            : base(unitOfWork)
        {
            _uow = unitOfWork;
            _expenseTypeRepository = expenseTypeRepository;
        }

        public async Task<int> Handle(CreateExpenseTypeCommand request, CancellationToken cancellationToken)
        {
            var expenseType = new ExpenseType
            {
                Name = request.Name,
                IsFixed = request.IsFixed
            };

            _expenseTypeRepository.Add(expenseType);

            await _uow.CommitAsync();

            return expenseType.Id;
        }
    }
}
