using Models.Entities;

namespace Models.DTO
{
    public class GuitarDTO
    {
        /// <summary>
        /// Id of the Guitar
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Guitar Model
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Price of the Guitar
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Number of Guitar Pickups
        /// </summary>
        public int NumOfPickups { get; set; }

        /// <summary>
        /// Type of Guitars wood
        /// </summary>
        public string TypeOfWood { get; set; }

        /// <summary>
        /// Brand of the Guitar
        /// </summary>
        public Brand Brand { get; set; }
        /// <summary>
        /// Category of the Guitar
        /// </summary>
        public Category Category { get; set; }
    }
}
