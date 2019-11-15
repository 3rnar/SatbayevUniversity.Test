using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Amount { get; set; }
        public int Sum { get; set; }
        public DateTime Date { get; set; }
        public string RegionName { get; set; }
    }
}
