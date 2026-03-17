using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public DiscountEnum Type { get; set; }
        public decimal Value { get; set; }

        public int GuitarId { get; set; }
    }
}
