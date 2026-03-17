using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("discounts")]
        [AllowAnonymous]
        public async Task<List<DiscountDTO>> GetAllDiscounts()
        {
            return await Task.FromResult(_discountService.GetAllDiscounts());
        }

        [HttpGet("get-discount-price")]
        [AllowAnonymous]
        public async Task<decimal> GetDiscountedPrice(int guitarId)
        {
            return await Task.FromResult(_discountService.GetDiscountedPrice(guitarId));
        }

        [HttpPost("add-discount")]
        [Authorize(Policy = "SuperRights")]
        public async Task<int> AddDiscount([FromBody] DiscountDTO discountDTO)
        {
            return await _discountService.AddDiscount(discountDTO);
        }
    }
}
