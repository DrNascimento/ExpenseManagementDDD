using Application.Interfaces;
using Application.ViewModel.ExpenseInstallment;
using AutoMapper;
using Domain.Commands.ExpenseInstallmentCommands;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace Application.Services;

public class ExpenseInstallmentAppService(IUserContext userContext,
    IMapper mapper,
    IExpenseInstallmentRepository expenseInstallmentRepository,
    IMediator mediator) : IExpenseInstallmentAppService
{
    private readonly IUserContext _userContext = userContext;
    private readonly IMapper _mapper = mapper;
    private readonly IExpenseInstallmentRepository _expenseInstallmentRepository = expenseInstallmentRepository;
    private readonly IMediator _mediator = mediator;

    public async Task<ExpenseInstallmentViewModel> Get(Guid id)
    {
        var expenseInstallment = await _expenseInstallmentRepository.GetExpenseInstallment(id, _userContext.UserId);
        return _mapper.Map<ExpenseInstallmentViewModel>(expenseInstallment);
    }

    public IEnumerable<ExpenseInstallmentViewModel> GetByDate(int year, int month, int day)
    {
        var expensesInstallments = 
            _expenseInstallmentRepository.GetExpenseInstallments(_userContext.UserId)
                .Where(e => 
                    e.DueDate.Year == year
                    && (month == 0 || e.DueDate.Month == month)
                    && (day == 0 || e.DueDate.Day == day)
                ).OrderBy(e => e.InstallmentNumber);

        return _mapper.Map<IEnumerable<ExpenseInstallmentViewModel>>(expensesInstallments);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task Update(Guid id, UpdateExpenseInstallmentViewModel updateExpenseInstallmentViewModel)
    {
        updateExpenseInstallmentViewModel.Id = id;
        updateExpenseInstallmentViewModel.UserId = _userContext.UserId;
        var command = _mapper.Map<UpdateExpenseInstallmentCommand>(updateExpenseInstallmentViewModel);

        await _mediator.Send(command);
    }

    public async Task UpdatePaid(Guid id)
    {
        var command = new TogglePaidExpenseInstallmentCommand(id, _userContext.UserId);
        await _mediator.Send(command);
    }

    public async Task Delete(Guid id)
    {
        var command = new DeleteExpenseInstallmentCommand(id, _userContext.UserId);
        await _mediator.Send(command);
    }
}
