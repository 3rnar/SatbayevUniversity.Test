using Dapper;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using S.U.TEST.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;

namespace S.U.TEST.Repositories
{
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(IOptionsMonitor<DataBaseConfig> optionsAccessor) : base(optionsAccessor)
        {
        }

        public IEnumerable<OrderViewModel> GetOrdersPagination(int page, int size, string sort)
        {
            var query = "sp_pagination2";
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@page", page);
            queryParameters.Add("@size", size);
            queryParameters.Add("@sort", sort);
            queryParameters.Add("@totalRow", 0);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<OrderViewModel>(query, queryParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            var query = "sp_orders";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<OrderViewModel>(query, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public void DeleteOrder(int id)
        {
            var order = base.SingleQuery(id);
            base.Delete(order);
        }

        public IEnumerable<OrderViewModel> FindOrderByProductNameAndRegion(string word)
        {
            var query = "sp_finder";
            var queryParameters = new DynamicParameters();
            queryParameters.Add("@word",word);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var result = connection.Query<OrderViewModel>(query, queryParameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public string ExportDataToExcel(FileInfo file)
        {
            using (ExcelPackage package = new ExcelPackage(file))
            {

                OrderViewModel[] customerList = GetAllOrders().ToArray();

                if (package.Workbook.Worksheets.Any(x => x.Name == "Orders"))
                {
                    package.Workbook.Worksheets.Delete("Orders");
                }
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Orders");
                int totalRows = customerList.Count();

                var columns = typeof(OrderViewModel)
                                    .GetProperties()
                                    .Where(e => !e.PropertyType.GetTypeInfo().IsGenericType)
                                    .Select(e => e.Name).ToList();

                for (int j = 0; j < columns.Count; j++)
                {
                    worksheet.Cells[1, j+1].Value = columns[j];
                }

                int i = 0;
                for (int row = 2; row <= totalRows + 1; row++)
                {

                    worksheet.Cells[row, 1].Value = customerList[i].Id;
                    worksheet.Cells[row, 2].Value = customerList[i].Product;
                    worksheet.Cells[row, 3].Value = customerList[i].Amount;
                    worksheet.Cells[row, 4].Value = customerList[i].Sum;
                    worksheet.Cells[row, 5].Value = customerList[i].Date;
                    worksheet.Cells[row, 6].Value = customerList[i].RegionName;
                    i++;

                }

                package.Save();

            }
            return "Your data is exported Successfully into Excel File.";
        }

    }
}
