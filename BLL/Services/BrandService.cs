using Abstraction.Interfaces.DataSourse;
using Abstraction.Interfaces.Services;
using Mapster;
using Models.DTO;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class BrandService : IBrandService
    {
        // берём из бд таблицу с нужными нам данными
        private readonly IGenericDataSourse<Brand> _BrandDataSource;

        public BrandService(IGenericDataSourse<Brand> brandDataSource)
        {
            _BrandDataSource = brandDataSource;
        }

        public List<BrandDTO> GetAllBrands()
        {
            return _BrandDataSource.GetElements()
                .ProjectToType<BrandDTO>() // Mapper
                .ToList();
        }

        public async Task<int> AddBrand(BrandDTO brandDTO)
        {
            var brand = brandDTO.Adapt<Brand>();
            await _BrandDataSource.AddAsync(brand);
            await _BrandDataSource.SaveChangesAsync();
            return brandDTO.Id;
        }

        public async Task<int> UpdateBrand(BrandDTO brandDTO)
        {
            var brand = brandDTO.Adapt<Brand>();
            await _BrandDataSource.UpdateBase(brand);
            await _BrandDataSource.SaveChangesAsync();
            return brandDTO.Id;
        }

        public async Task<int> DeleteBrand(int id)
        {
            var brandToDelete = _BrandDataSource.GetElements().FirstOrDefault(x => x.Id == id);
            if (brandToDelete == null)
            {
                throw new Exception();
            }
            await _BrandDataSource.RemoveAsync(brandToDelete);
            await _BrandDataSource.SaveChangesAsync();
            return id;
        }
    }
}
