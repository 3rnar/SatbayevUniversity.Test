using System.Collections.Generic;
using System.IO;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;

namespace S.U.TEST.Repositories
{
    public interface IOrdersRepository : IRepositoryBase<Order>
    {
        void DeleteOrder(int id);
        IEnumerable<OrderViewModel> GetOrdersPagination(int page, int size, string sort);
        IEnumerable<OrderViewModel> GetAllOrders();
        string ExportDataToExcel(FileInfo file);
    }
}