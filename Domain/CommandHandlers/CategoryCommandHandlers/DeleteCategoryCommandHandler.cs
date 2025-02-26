using Domain.Commands.Category;
using Domain.Exceptions;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.CategoryCommandHandlers;

public class DeleteCategoryCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, 
        ICategoryRepository categoryRepository) : base(unitOfWork)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetById(request.Id)
            ?? throw new ResourceNotFoundException("Category not found");

        _categoryRepository.Delete(category);

        await _uow.CommitAsync();

        return Unit.Value;
    }
}
