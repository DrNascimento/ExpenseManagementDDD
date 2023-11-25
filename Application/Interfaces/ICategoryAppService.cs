using Application.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryAppService : IDisposable
    {
        Task<CategoryViewModel> Get(int id);

        IEnumerable<CategoryViewModel> GetAll();

        Task<int> CreateUniversal(CreateCategoryViewModel createCategoryViewModel);

        Task<int> CreateByUser(CreateCategoryViewModel createCategoryViewModel);

        Task Update(int id, UpdateCategoryViewModel updateCategoryViewModel);

        Task Delete(int id);
    }
}
