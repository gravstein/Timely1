using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CategoryDTO
    {
        /// <summary>
        /// Id of the Category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the Category
        /// </summary>
        public string Description { get; set; }
    }
}
