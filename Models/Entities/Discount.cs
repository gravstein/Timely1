using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Discount
    {
        public int Id { get; set; }
        public DiscountEnum Type { get; set; }
        public decimal Value { get; set; }

        public int GuitarId { get; set; }
        public Guitar Guitar { get; set; }
    }
}
