using Application.Interfaces;
using Application.ViewModel.ExpenseType;
using AutoMapper;
using Domain.Commands.ExpenseTypeCommands;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.Services;

public class ExpenseTypeAppService(IMapper mapper,
    IMediator mediator,
    IExpenseTypeRepository expenseTypeRepository) : IExpenseTypeAppService
{
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;
    private readonly IExpenseTypeRepository _expenseTypeRepository = expenseTypeRepository;
    private bool disposedValue;

    public async Task<ExpenseTypeViewModel> GetById(Guid id)
    {
        var expense = await _expenseTypeRepository.GetById(id);
        return _mapper.Map<ExpenseTypeViewModel>(expense);
    }

    public IEnumerable<ExpenseTypeViewModel> GetAll()
    {
        var expenses = _expenseTypeRepository.GetAll();

        return _mapper.Map<IEnumerable<ExpenseTypeViewModel>>(expenses);
    }

    public async Task<Guid> Create(ExpenseTypeViewModel expenseTypeViewModel)
    {
        var command = _mapper.Map<CreateExpenseTypeCommand>(expenseTypeViewModel);

        return await _mediator.Send(command);
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
