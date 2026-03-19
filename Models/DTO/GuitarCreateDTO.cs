using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    // чтобы добавлять гитары просто по айдишникам
    public class GuitarCreateDTO
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
        /// Path to image of guitar
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Id of Brand of the Guitar
        /// </summary>
        public int BrandId { get; set; }
        /// <summary>
        /// Id of Category of the Guitar
        /// </summary>
        public int CategoryId { get; set; }
    }
}
