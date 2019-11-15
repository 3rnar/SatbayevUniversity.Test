using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;
using S.U.TEST.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S.U.TEST.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public IActionResult Registration([FromBody]Registration newUser)
        {
            try
            {
                _usersService.RegistrationOfNewUser(newUser);
                return Ok();
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]Auth authUser)
        {
            try
            {
                var user = _usersService.AuthUser(authUser);
                return Ok(user);
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _usersService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception aue)
            {
                return BadRequest(aue.Message);
            }

        }
        [Authorize(Roles = Role.Administrator)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _usersService.GetUserByID(id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Administrator))
            {
                return Forbid();
            }

            return Ok(user);
        }
    }
}
