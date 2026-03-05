using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using Mapster;
using Models.DTO;
using Models.Entities;

namespace BLL.Services
{
    public class GuitarService : IGuitarService
    {
        // берём из бд таблицу с нужными нам данными
        private readonly IGenericDataSourse<Guitar> _GuitarDataSource;

        public GuitarService(IGenericDataSourse<Guitar> guitarDataSource)
        {
            _GuitarDataSource = guitarDataSource;
        }

        public List<GuitarDTO> GetAllGuitars()
        {
            return _GuitarDataSource.GetElements()
                .ProjectToType<GuitarDTO>() // Mapper
                .ToList();
        }

        public async Task<int> AddGuitar(GuitarDTO guitarDTO)
        {
            var guitar = guitarDTO.Adapt<Guitar>();
            await _GuitarDataSource.AddAsync(guitar);
            await _GuitarDataSource.SaveChangesAsync();
            return guitarDTO.Id;
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
