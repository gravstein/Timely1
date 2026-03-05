using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BrandDTO
    {
        /// <summary>
        /// Id of the Brand
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the Brand
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Year Founded of the Brand
        /// </summary>
        public int YearFounded { get; set; }

        /// <summary>
        /// Country of the Brand
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Official Site of the Brand
        /// </summary>
        public string OfficialSite { get; set; }
    }
}
