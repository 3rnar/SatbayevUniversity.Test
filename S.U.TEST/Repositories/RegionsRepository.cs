using Dapper;
using Microsoft.Extensions.Options;
using S.U.TEST.Models;
using S.U.TEST.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace S.U.TEST.Repositories
{
    public class RegionsRepository : RepositoryBase<Region>, IRegionsRepository
    {
        public RegionsRepository(IOptionsMonitor<DataBaseConfig> optionsAccessor) : base(optionsAccessor)
        {
        }
        public Region GetByRegionName(string name)
        {
            var query = $"SELECT * FROM users WHERE RegionName = @RegionName";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var s = connection.Query<Region>(query, new { RegionName = name }).SingleOrDefault();
                return s;
            }
        }

        public IEnumerable<Region> GetRegions()
        {
            var query = $"sp_recursion";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Region>(query, commandType: CommandType .StoredProcedure);
            }
        }

        public void DeleteRegion(int id)
        {
            var item = base.SingleQuery(id);
            base.Delete(item);
        }
    }
}
