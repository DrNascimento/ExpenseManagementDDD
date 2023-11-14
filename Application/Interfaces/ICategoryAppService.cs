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
    }
}
