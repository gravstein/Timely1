using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.Interfaces.Services
{
    public interface IBrandService
    {
        /// <summary>
        /// Method for getting all Brands
        /// </summary>
        /// <returns>List of Brands</returns>
        List<BrandDTO> GetAllBrands();

        /// <summary>
        /// Method for adding a new Brand
        /// </summary>
        /// <param name="brandDTO">Brand data to add</param>
        /// <returns>Id of added brandDTO</returns>
        Task<int> AddBrand(BrandDTO brandDTO);

        /// <summary>
        /// Method for updating an Brand
        /// </summary>
        /// <param name="brandDTO">New brand data</param>
        /// <returns>Id of updated brandDTO</returns>
        Task<int> UpdateBrand(BrandDTO brandDTO);

        /// <summary>
        /// Method for deleting a Brand
        /// </summary>
        /// <param name="id">id of the brand to delete</param>
        /// <returns>Id of deleted BrandDTO</returns>
        Task<int> DeleteBrand(int id);
    }
}
