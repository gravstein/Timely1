using Abstraction.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Timely1.Controllers
{
    [ApiController] // говорим что это API-контроллер
    [Route("api/[controller]")] // mapping. то есть тут задаётся по какому url-нику
                                // мы должны сделать запрос чтобы обратиться к этому контроллеру
                                // [controller] замениться на имя класса без слова Controller
                                // по итогу: https://site.com/.../api/Guitar
    //[Authorize] // с этим могут работать только аутентифицированные пользователи
    public class GuitarController : ControllerBase
    {
        private readonly IGuitarService guitarService; // храним сервис
        
        // Dependency Injection. тут мы внедрили зависимость нашего контроллера от сервиса
        public GuitarController(IGuitarService guitarService) 
        {
            this.guitarService = guitarService;
        }

        [HttpGet("guitars")] // метод GET который будет находится по пути: .../api/Guitar/guitars
        [AllowAnonymous]
        public async Task<List<GuitarDTO>> GetAllGuitars()
        {
            return await Task.FromResult(guitarService.GetAllGuitars()); // FromResult оборачивает наш результат в таску
            // здесь возвращаем Task потому что у нас этот метод в сервисе возвращает List
        }

        [HttpPost("add-guitar")]
        [Authorize(Policy = "SuperRights")] // только менеджеры и админы
        public async Task<int> AddGuitar([FromBody] GuitarCreateDTO guitar) // брать данные из Body запроса а не из самой ссылки
        {
            return await guitarService.AddGuitar(guitar);
        }

        [HttpPut("update-guitar")]
        [Authorize(Policy = "SuperRights")]
        public async Task<int> UpdateGuitar([FromBody] GuitarCreateDTO guitar) 
        {
            return await guitarService.UpdateGuitar(guitar);
        }

        [HttpPut("image-guitar")]
        [Authorize(Policy = "SuperRights")] // только админы
        public async Task<int> UploadImage([FromQuery] int id, IFormFile img)
        {
            return await guitarService.UploadImage(id,img);
        }

        [HttpDelete("delete-guitar")]
        [Authorize(Policy = "SuperMegaRights")] // только админы
        public async Task<int> DeleteGuitar(int id)
        {
            return await guitarService.DeleteGuitar(id);
        }
    }
}
    