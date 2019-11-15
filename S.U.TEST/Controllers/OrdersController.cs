using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using S.U.TEST.Models;
using S.U.TEST.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrdersService _ordersService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public OrdersController(IOrdersService ordersService, IHostingEnvironment hostingEnvironment)
        {
            _ordersService = ordersService;
            _hostingEnvironment = hostingEnvironment;
        }


        // GET api/Orders/5/10/DATE+ASC
        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        [Route("{page}/{size}/{sort}")]
        public IActionResult GetOrdersPagination(int page, int size, string sort)
        {
            try
            {
                return Ok(_ordersService.GetOrdersPagination(page, size, sort));
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // POST api/Orders
        [Authorize(Roles = Role.Customer)]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            try
            {
                _ordersService.InsertOrder(order);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // GET api/Orders
        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_ordersService.GetAllOrders());
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // GET api/Users/ExportToExcel
        [Authorize(Roles = Role.Administrator)]
        [HttpGet("ExportToExcel")]
        public IActionResult ExportEmployeesToExcel()
        {
            try
            {
                string rootFolder = _hostingEnvironment.WebRootPath;

                string fileName = @"ExportOrders.xlsx";

                if (string.IsNullOrWhiteSpace(rootFolder))
                {
                    rootFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                return Ok(_ordersService.ExportDataToExcel(file));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpGet("Search/{word}")]
        public IActionResult SearchOrder(string word)
        {
            try
            {
                return Ok(_ordersService.FindOrderByProductNameAndRegion(word));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
