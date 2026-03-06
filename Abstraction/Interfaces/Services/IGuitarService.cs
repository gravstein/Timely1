using Models.DTO;

namespace Abstraction.Interfaces.Services
{
    // Интерфейс где описывается сервис для модели Guitar. Реализуется в слое BLL в соответсвующем файле GuitarService.cs

    public interface IGuitarService
    {
        /// <summary>
        /// Method for getting all Guitars
        /// </summary>
        /// <returns>List of Guitars</returns>
        List<GuitarDTO> GetAllGuitars();

        /// <summary>
        /// Method for adding a new Guitar
        /// </summary>
        /// <param name="guitarDTO">Guitar data to add</param>
        /// <returns>Id of added guitarDTO</returns>
        Task<int> AddGuitar(GuitarDTO guitarDTO);

        /// <summary>
        /// Method for updating an Guitar
        /// </summary>
        /// <param name="guitarDTO">New guitar data</param>
        /// <returns>Id of updated guitarDTO</returns>
        Task<int> UpdateGuitar(GuitarDTO guitarDTO);

        /// <summary>
        /// Method for deleting a Guitar
        /// </summary>
        /// <param name="id">id of the guitar to delete</param>
        /// <returns>Id of deleted GuitarDTO</returns>
        Task<int> DeleteGuitar(int id);
    }
}
