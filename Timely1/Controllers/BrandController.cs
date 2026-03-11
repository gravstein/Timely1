using Abstraction.Interfaces.Services;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<List<BrandDTO>> GetAllBrands()
        {
            return await Task.FromResult(brandService.GetAllBrands());
        }

        [HttpPost("add-brand")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<int> AddBrand([FromBody] BrandDTO brand)
        {
            return await brandService.AddBrand(brand);
        }

        [HttpPut("update-brand")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<int> UpdateBrand([FromBody] BrandDTO brand)
        {
            return await brandService.UpdateBrand(brand);
        }

        [HttpDelete("delete-brand")]
        [Authorize(Roles = "Admin")]
        public async Task<int> DeleteBrand(int id)
        {
            return await brandService.DeleteBrand(id);
        }
    }
}
