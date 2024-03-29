﻿using Microsoft.AspNetCore.Authorization;
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
    public class RegionsController : ControllerBase
    {
        IRegionsRepository _regionsRepository;

        public RegionsController(IRegionsRepository regionsRepository)
        {
            _regionsRepository = regionsRepository;
        }


        // GET api/Regions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            try
            {
                return Ok(_regionsRepository.GetRegions());
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // GET api/Regions/5
        [HttpGet("{id}")]
        public IActionResult GetRegion(int id)
        {
            try
            {
                return Ok(_regionsRepository.SingleQuery(id));
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // POST api/Regions
        [HttpPost]
        public IActionResult CreateRegion([FromBody] Region region)
        {
            try
            {
                _regionsRepository.Insert(region);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }


        // PUT api/Regions
        [HttpPut]
        public IActionResult UpdateRegion([FromBody] Region region)
        {
            try
            {
                _regionsRepository.Update(region);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }



        // DELETE api/Regions
        [HttpDelete]
        public IActionResult DeleteRegion(int id)
        {
            try
            {
                _regionsRepository.DeleteRegion(id);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }
    }
}
