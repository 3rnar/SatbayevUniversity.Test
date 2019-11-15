using Dapper;
using Microsoft.Extensions.Options;
using S.U.TEST.Models;
using S.U.TEST.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Repositories
{
    public class ItemsRepository : RepositoryBase<Item>, IItemsRepository
    {
        public ItemsRepository(IOptionsMonitor<DataBaseConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public Item GetByItemName(string itemName)
        {
            var query = $"SELECT * FROM items WHERE ItemName = @ItemName";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Item>(query, new { ItemName = itemName }).SingleOrDefault();
            }
        }

        public void DeleteItem(int id)
        {
            var item = base.SingleQuery(id);
            base.Delete(item);
        }
    }
}
