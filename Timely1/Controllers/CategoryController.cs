using Abstraction.Interfaces.Services;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        protected readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("categories")]
        [Authorize]
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await Task.FromResult(categoryService.GetAllCategories());
        }

        [HttpPost("add-category")]
        [Authorize(Policy = "SuperRights")]
        public async Task<int> AddCategory([FromBody] CategoryDTO category)
        {
            return await categoryService.AddCategory(category);
        }

        [HttpPut("update-category")]
        [Authorize(Policy = "SuperRights")]
        public async Task<int> UpdateCategory([FromBody] CategoryDTO category)
        {
            return await categoryService.UpdateCategory(category);
        }

        [HttpDelete("delete-category")]
        [Authorize(Policy = "SuperMegaRights")]
        public async Task<int> DeleteCategory(int id)
        {
            return await categoryService.DeleteCategory(id);
        }
    }
}
