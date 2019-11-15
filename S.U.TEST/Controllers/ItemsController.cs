using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S.U.TEST.Models;
using S.U.TEST.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Controllers
{
    [Authorize(Roles = Role.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        IItemsRepository _itemsRepository;

        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        // GET api/Items
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            try
            {
                return Ok(_itemsRepository.Query());
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // GET api/Items/5
        [HttpGet("{id}")]
        public IActionResult GetRegion(int id)
        {
            try
            {
                return Ok(_itemsRepository.SingleQuery(id));
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // POST api/Items
        [HttpPost]
        public IActionResult CreateRegion([FromBody] Item item)
        {
            try
            {
                _itemsRepository.Insert(item);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // PUT api/Items
        [HttpPut]
        public IActionResult UpdateRegion([FromBody] Item item)
        {
            try
            {
                _itemsRepository.Update(item);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // DELETE api/Items
        [HttpDelete]
        public IActionResult DeleteRegion(int id)
        {
            try
            {
                _itemsRepository.DeleteItem(id);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }
    }
}
