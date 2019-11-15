using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using System.Collections.Generic;
using System.IO;

namespace S.U.TEST.Services
{
    public interface IOrdersService
    {
        IEnumerable<OrderViewModel> GetOrdersPagination(int page, int size, string sort);
        IEnumerable<OrderViewModel> GetAllOrders();
        void InsertOrder(Order order);
        string ExportDataToExcel(FileInfo file);
        IEnumerable<OrderViewModel> FindOrderByProductNameAndRegion(string word);
    }
}