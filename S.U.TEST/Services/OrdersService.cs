using Microsoft.AspNetCore.Mvc;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using S.U.TEST.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Services
{
    public class OrdersService : IOrdersService
    {
        IOrdersRepository _ordersRepository;
        IItemsRepository _itemsRepository;
        IRegionsRepository _regionsRepository;

        public OrdersService(IOrdersRepository ordersRepository, IItemsRepository itemsRepository, IRegionsRepository regionsRepository)
        {
            _ordersRepository = ordersRepository;
            _itemsRepository = itemsRepository;
            _regionsRepository = regionsRepository;
        }

        public void InsertOrder(Order order)
        {
            order.Date = DateTime.Now;
            var product = _itemsRepository.SingleQuery(order.ProductId);
            order.Sum = order.Amount * product.Price;

            _ordersRepository.Insert(order);
        }

        public IEnumerable<OrderViewModel> GetOrdersPagination(int page, int size, string sort)
        {
            return _ordersRepository.GetOrdersPagination(page,size,sort);
        }

        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            return _ordersRepository.GetAllOrders();
        }
        public string ExportDataToExcel(FileInfo file)
        {
            return _ordersRepository.ExportDataToExcel(file);
        }
    }
}
