using Domain.Commands.Category;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers.CategoryCommandHandlers
{
    public class CreateCategoryCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository) 
            : base(unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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
}
