using Application.Interfaces;
using Application.ViewModel.Category;
using AutoMapper;
using Domain.Commands.Category;
using Domain.Enums;
using Domain.Interfaces.Repository;
using Infrastructure.CrossCutting.Identity;
using MediatR;

namespace Application.Services;

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

    public async Task<CategoryViewModel> Get(Guid id)
    {
        var category = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryViewModel>(category);
    }

    public IEnumerable<CategoryViewModel> GetAll()
    {
        var categories = _categoryRepository.GetUsersCategories(_userContext.UserId);
        return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
    }

    public async Task<Guid> CreateUniversal(CreateCategoryViewModel createCategoryViewModel)
        => await Create(createCategoryViewModel);

    public async Task<Guid> CreateByUser(CreateCategoryViewModel createCategoryViewModel)
    {
        createCategoryViewModel.UserId = _userContext.UserId;

        return await Create(createCategoryViewModel);
    }

    private async Task<Guid> Create(CreateCategoryViewModel createCategoryViewModel)
    {
        var command = _mapper.Map<CreateCategoryCommand>(createCategoryViewModel);
        return await _mediator.Send(command);
    }

    public async Task Update(Guid id, UpdateCategoryViewModel updateCategoryViewModel)
    {
        await ValidatePermission(id);

        updateCategoryViewModel.Id = id;
        var command = _mapper.Map<UpdateCategoryCommand>(updateCategoryViewModel);
        await _mediator.Send(command);
    }

    public async Task Delete(Guid id)
    {
        await ValidatePermission(id);
        await _mediator.Send(new DeleteCategoryCommand(id));
    }


    private async Task ValidatePermission(Guid id)
    {
        var category = await _categoryRepository.GetById(id)
            ?? throw new InvalidOperationException("Category not found.");

        if (category.UserId is null &&
            _userContext.Role.Equals(nameof(UserTypeEnum.Admin), StringComparison.OrdinalIgnoreCase) &&
            _userContext.UserId != category.UserId)
            throw new InvalidOperationException("It's not possible edit this Category.");
    }

    public async Task<CategoriesSummaryViewModel> Summary(DateTime start, DateTime end)
    {
        var summaryCategoriesView = await _categoryRepository.Summary(start, end, _userContext.UserId);

        return _mapper.Map<CategoriesSummaryViewModel>(summaryCategoriesView);   
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
