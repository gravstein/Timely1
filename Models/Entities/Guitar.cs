using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Guitar
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public decimal Price { get; set; }
        public int NumOfPickups { get; set; }
        public string TypeOfWood { get; set; }
        public string ImagePath { get; set; }

        public int BrandId { get; set; } // FK поле по нему EF сам найдёт бренд
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
