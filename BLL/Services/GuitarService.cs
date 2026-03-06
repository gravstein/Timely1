using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using Mapster;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class GuitarService : IGuitarService
    {
        // берём из бд таблицу с нужными нам данными используя универсальный класс 
        private readonly IGenericDataSourse<Guitar> _GuitarDataSource;

        public GuitarService(IGenericDataSourse<Guitar> guitarDataSource)
        {
            _GuitarDataSource = guitarDataSource;
        }

        public List<GuitarDTO> GetAllGuitars()
        {
            return _GuitarDataSource.GetElements()
                .ProjectToType<GuitarDTO>() 
                .ToList();
            // Mapper. метод ProjectToType<DTO> работает с IQueryable, то есть сразу со множеством элементов 
            // и тянет из базы только те поля которые описаны в DTO 
        }

        public async Task<int> AddGuitar(GuitarDTO guitarDTO)
        {
            var guitar = guitarDTO.Adapt<Guitar>(); 
            // mapper. просто адаптирует нашу модель внутрь DTO-шки, вместо того чтобы вручную приравнивать все поля
            // метод Adapt<Model> работает только с одним объектом, тянет из базы все его поля и заносит в DTO нужные
            
            await _GuitarDataSource.AddAsync(guitar);
            await _GuitarDataSource.SaveChangesAsync();
            return guitar.Id;
        }

        public async Task<int> UpdateGuitar(GuitarDTO guitarDTO)
        {
            var guitar = guitarDTO.Adapt<Guitar>();
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
    }
}
