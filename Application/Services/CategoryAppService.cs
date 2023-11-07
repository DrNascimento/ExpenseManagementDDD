using Application.Interfaces;
using Application.ViewModel.Category;
using AutoMapper;
using Domain.Interfaces.Repository;

namespace Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private bool disposedValue;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryAppService(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryViewModel> Get(int id)
        {
            var category = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryViewModel>(category);
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
