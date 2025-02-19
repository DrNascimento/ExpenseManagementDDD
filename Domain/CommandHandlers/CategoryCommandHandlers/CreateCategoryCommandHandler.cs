using Domain.Commands.Category;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;

namespace Domain.CommandHandlers.CategoryCommandHandlers;

public class CreateCategoryCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork,
        ICategoryRepository categoryRepository) 
        : base(unitOfWork)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            UserId = request.UserId,
        };

        _categoryRepository.Add(category);

        await _uow.CommitAsync();

        return category.Id;
    }
}
