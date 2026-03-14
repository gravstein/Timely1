using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using Mapster;
using Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericDataSourse<Category> _CategoryDataSource;

        public CategoryService(IGenericDataSourse<Category> categoryDataSource)
        {
            _CategoryDataSource = categoryDataSource;
        }

        public List<CategoryDTO> GetAllCategories()
        {
            return _CategoryDataSource.GetElements()
                .ProjectToType<CategoryDTO>() // Mapper
                .ToList();
        }

        public async Task<int> AddCategory(CategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Category>();
            await _CategoryDataSource.AddAsync(category);
            await _CategoryDataSource.SaveChangesAsync();
            return categoryDTO.Id;
        }

        public async Task<int> UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = categoryDTO.Adapt<Category>();
            await _CategoryDataSource.UpdateBase(category);
            await _CategoryDataSource.SaveChangesAsync();
            return categoryDTO.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            var categoryToDelete = _CategoryDataSource.GetElements().FirstOrDefault(x => x.Id == id);
            if (categoryToDelete == null)
            {
                throw new Exception();
            }
            await _CategoryDataSource.RemoveAsync(categoryToDelete);
            await _CategoryDataSource.SaveChangesAsync();
            return id;
        }
    }
}
