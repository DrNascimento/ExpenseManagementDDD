using Application.ViewModel.Category;

namespace Application.Interfaces;

public interface ICategoryAppService : IDisposable
{
    Task<CategoryViewModel> Get(Guid id);

    IEnumerable<CategoryViewModel> GetAll();

    Task<Guid> CreateUniversal(CreateCategoryViewModel createCategoryViewModel);

    Task<Guid> CreateByUser(CreateCategoryViewModel createCategoryViewModel);

    Task Update(Guid id, UpdateCategoryViewModel updateCategoryViewModel);

    Task Delete(Guid id);

    Task<CategoriesSummaryViewModel> Summary(DateTime start, DateTime end);
}
