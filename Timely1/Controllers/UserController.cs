using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<int> Register([FromBody] RegistrationDTO registrationDTO)
        {
            if (registrationDTO == null)
                throw new ArgumentNullException("Registration data is required");

            var result = await userService.RegisterUser(registrationDTO);
            return result;
        }
    }
}
