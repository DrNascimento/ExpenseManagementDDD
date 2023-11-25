﻿using Domain.Commands.Category;
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
    public class UpdateCategoryCommandHandler : UnitOfWorkCommandHandler, IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository) : base(unitOfWork)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id)
                ?? throw new InvalidOperationException("Category not found.");

            category.Name = request.Name;
            
            _categoryRepository.Update(category);

            await _uow.CommitAsync();

            return Unit.Value;
        }
    }
}