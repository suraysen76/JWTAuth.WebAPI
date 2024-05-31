
using JWTAuth.WebAPI.Interfaces;
using JWTAuth.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JWTAuth.WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _IUsers;

        public UserController(IUsers IUsers)
        {
            _IUsers = IUsers;
        }

        // GET: api/user>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            return await Task.FromResult(_IUsers.GetUserDetails());
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var users = await Task.FromResult(_IUsers.GetUserDetails(id));
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        // POST api/User
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel user)
        {
            _IUsers.AddUser(user);
            return await Task.FromResult(user);
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(int id, UserModel user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            try
            {
                _IUsers.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(user);
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> Delete(int id)
        {
            var user = _IUsers.DeleteUser(id);
            return await Task.FromResult(user);
        }

        private bool UserExists(int id)
        {
            return _IUsers.CheckUser(id);
        }
       
    }
}