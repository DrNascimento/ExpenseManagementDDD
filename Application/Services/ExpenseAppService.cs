using Application.Interfaces;
using Application.ViewModel.Expense;
using AutoMapper;
using Domain.Commands.ExpenseCommands;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace Application.Services;

public class ExpenseAppService(IExpenseRepository expenseRepository,
    IMediator mediator,
    IMapper mapper,
    IUserContext userContext) : IExpenseAppService
{
    private bool disposedValue;
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IUserContext _userContext = userContext;

    public async Task<ExpenseViewModel> Get(Guid id)
    {
        var expense = await _expenseRepository.GetById(id);

        return _mapper.Map<ExpenseViewModel>(expense);
    }

    public IEnumerable<ExpenseViewModel> GetAll()
    {
        var expenses = _expenseRepository.GetExpenses(_userContext.UserId);
        return _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses);
    }

    public IEnumerable<ExpenseViewModel> GetByDate(int year, int month, int day)
    {
        var expenses = _expenseRepository.GetByDate(year, month, day);

        return _mapper.Map<IEnumerable<ExpenseViewModel>>(expenses);
    }

    public async Task<Guid> Create(CreateExpenseViewModel createExpenseViewModel)
    {
        createExpenseViewModel.UserId = _userContext.UserId;
        var command = _mapper.Map<CreateExpenseCommand>(createExpenseViewModel);
        return await _mediator.Send(command);
    }

    public async Task Update(Guid id, UpdateExpenseViewModel updateExpenseViewModel)
    {
        await ValidatePermission(id);

        updateExpenseViewModel.Id = id;
        
        var command = _mapper.Map<UpdateExpenseCommand>(updateExpenseViewModel);
        await _mediator.Send(command);
    }

    public async Task Delete(Guid id)
    {
        await ValidatePermission(id);
        await _mediator.Send(new DeleteExpenseCommand(id));
    }

    public async Task ValidatePermission(Guid id)
    {
        var expense = await _expenseRepository.GetById(id);

        if (expense is null || _userContext.UserId != expense.UserId)
            throw new InvalidOperationException("Expense not found");

    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
