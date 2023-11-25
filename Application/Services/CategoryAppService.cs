using Application.Interfaces;
using Application.ViewModel.Category;
using AutoMapper;
using Domain.Commands.Category;
using Domain.Enums;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private bool disposedValue;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;

        public CategoryAppService(ICategoryRepository categoryRepository,
            IMapper mapper,
            IMediator mediator,
            IUserContext userContext
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _mediator = mediator;
            _userContext = userContext;
        }

        public async Task<CategoryViewModel> Get(int id)
        {
            var category = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            int userId = Convert.ToInt16(_userContext.UserId);
            var categories = _categoryRepository.GetUsersCategories(userId);
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<int> CreateUniversal(CreateCategoryViewModel createCategoryViewModel)
            => await Create(createCategoryViewModel);

        public async Task<int> CreateByUser(CreateCategoryViewModel createCategoryViewModel)
        {
            createCategoryViewModel.UserId = Convert.ToInt16(_userContext.UserId);
            return await Create(createCategoryViewModel);
        }

        private async Task<int> Create(CreateCategoryViewModel createCategoryViewModel)
        {
            var command = _mapper.Map<CreateCategoryCommand>(createCategoryViewModel);
            return await _mediator.Send(command);
        }

        public async Task Update(int id, UpdateCategoryViewModel updateCategoryViewModel)
        {
            await ValidatePermission(id);

            updateCategoryViewModel.Id = id;
            var command = _mapper.Map<UpdateCategoryCommand>(updateCategoryViewModel);
            await _mediator.Send(command);
        }

        public async Task Delete(int id)
        {
            await ValidatePermission(id);
            await _mediator.Send(new DeleteCategoryCommand(id));
        }


        private async Task ValidatePermission(int id)
        {
            var category = await _categoryRepository.GetById(id)
                ?? throw new InvalidOperationException("Category not found.");

            if ((category.UserId is null && _userContext.Role != UserTypeEnum.Admin.ToString().ToLower())
                || Convert.ToInt16(_userContext.UserId) != category.UserId)
                throw new InvalidOperationException("You don't have permission.");
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
}
