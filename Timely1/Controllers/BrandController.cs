using Abstraction.Interfaces.Services;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        protected readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet("brands")]
        public async Task<List<BrandDTO>> GetAllBrands()
        {
            return await Task.FromResult(brandService.GetAllBrands());
        }

        [HttpPost("add-brand")]
        public async Task<int> AddBrand([FromBody] BrandDTO brand)
        {
            return await brandService.AddBrand(brand);
        }

        [HttpPut("update-brand")]
        public async Task<int> UpdateBrand([FromBody] BrandDTO brand)
        {
            return await brandService.UpdateBrand(brand);
        }

        [HttpDelete("delete-brand")]
        public async Task<int> DeleteBrand(int id)
        {
            return await brandService.DeleteBrand(id);
        }
    }
}
