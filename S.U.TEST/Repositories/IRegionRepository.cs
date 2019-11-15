using S.U.TEST.Models;
using System.Collections.Generic;

namespace S.U.TEST.Repositories
{
    public interface IRegionsRepository : IRepositoryBase<Region>
    {
        Region GetByRegionName(string name);
        IEnumerable<Region> GetRegions();
        void DeleteRegion(int id);
    }
}