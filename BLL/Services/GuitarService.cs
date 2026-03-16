using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using Common.Resources;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class GuitarService : IGuitarService
    {
        // берём из бд таблицу с нужными нам данными используя универсальный класс 
        private readonly IGenericDataSourse<Guitar> _GuitarDataSource;
        private readonly IWebHostEnvironment _env; // класс который позволяет работать с папками и файлами внутри веб среды
        private readonly IStringLocalizer<ErrorMessages> _localizer;

        public GuitarService(IGenericDataSourse<Guitar> guitarDataSource, IStringLocalizer<ErrorMessages> localizer, IWebHostEnvironment env)
        {
            _GuitarDataSource = guitarDataSource;
            _localizer = localizer;
            _env = env;
        }

        public List<GuitarDTO> GetAllGuitars()
        {
            return _GuitarDataSource.GetElements()
                .ProjectToType<GuitarDTO>() 
                .ToList();
            // Mapper. метод ProjectToType<DTO> работает с IQueryable, то есть сразу со множеством элементов 
            // и тянет из базы только те поля которые описаны в DTO 
        }

        public async Task<int> AddGuitar(GuitarCreateDTO guitarDTO)
        {
            var guitar = guitarDTO.Adapt<Guitar>();
            // mapper. просто адаптирует нашу модель внутрь DTO-шки, вместо того чтобы вручную приравнивать все поля
            // метод Adapt<Model> работает только с одним объектом, тянет из базы все его поля и заносит в DTO нужные
            
            guitar.BrandId = guitarDTO.BrandId;
            guitar.CategoryId = guitarDTO.CategoryId;

            await _GuitarDataSource.AddAsync(guitar);
            await _GuitarDataSource.SaveChangesAsync();
            return guitar.Id;
        }

        public async Task<int> UpdateGuitar(GuitarCreateDTO guitarDTO)
        {
            var guitar = guitarDTO.Adapt<Guitar>();

            guitar.BrandId = guitarDTO.BrandId;
            guitar.CategoryId = guitarDTO.CategoryId;

            await _GuitarDataSource.UpdateBase(guitar);
            await _GuitarDataSource.SaveChangesAsync();
            return guitarDTO.Id;
        }

        public async Task<int> DeleteGuitar(int id)
        {
            var guitarToDelete = _GuitarDataSource.GetElements().FirstOrDefault(x => x.Id == id);
            if (guitarToDelete == null)
            {
                throw new Exception();
            }
            await _GuitarDataSource.RemoveAsync(guitarToDelete);
            await _GuitarDataSource.SaveChangesAsync();
            return id;
        }

        public async Task<int> UploadImage(int guitarId, IFormFile img)
        {
            var guitar = _GuitarDataSource.GetElements().FirstOrDefault(x => x.Id == guitarId);
            if (guitar == null)
            {
                throw new ArgumentException(_localizer["GuitarNotFound"]);
            }

            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"); // получаем путь к папке wwwroot, если её нет то создаём её внутри папки с проектом
            var uploadPath = Path.Combine(webRoot, "images", "guitars");
            Directory.CreateDirectory(uploadPath); // создаём папку если её нет

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(img.FileName)}"; // генерируем уникальное имя для файла
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create)) // открываем поток для записи файла в using чтобы потом он закрылся и не блокировал файл
            {
                await img.CopyToAsync(stream); // записываем наш файл в stream и сохраняем его на сервере
            }

            guitar.ImagePath = $"/images/guitars/{fileName}"; // сохраняем путь к файлу в базе данных

            await _GuitarDataSource.SaveChangesAsync();
            return guitarId;
        }
    }
}
