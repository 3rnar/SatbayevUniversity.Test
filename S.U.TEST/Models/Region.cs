using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Models
{
    public class Region
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string RegionName { get; set; }
    }
}
