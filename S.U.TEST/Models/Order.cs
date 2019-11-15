using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Sum { get; set; }
        public DateTime Date { get; set; }
        public int RegionId { get; set; }
    }
}
