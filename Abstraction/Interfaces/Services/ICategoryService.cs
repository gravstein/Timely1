using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    public interface ICategoryService
    {
        /// <summary>
        /// Method for getting all Categories
        /// </summary>
        /// <returns>List of Categories</returns>
        List<CategoryDTO> GetAllCategories();

        /// <summary>
        /// Method for adding a new Category
        /// </summary>
        /// <param name="categoryDTO">Category data to add</param>
        /// <returns>Id of added categoryDTO</returns>
        Task<int> AddCategory(CategoryDTO categoryDTO);

        /// <summary>
        /// Method for updating an Category
        /// </summary>
        /// <param name="categoryDTO">New category data</param>
        /// <returns>Id of updated categoryDTO</returns>
        Task<int> UpdateCategory(CategoryDTO categoryDTO);

        /// <summary>
        /// Method for deleting a Category
        /// </summary>
        /// <param name="id">id of the category to delete</param>
        /// <returns>Id of deleted CategoryDTO</returns>
        Task<int> DeleteCategory(int id);
    }
}
