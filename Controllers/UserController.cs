using CRUD_BackEnd.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        public UserController(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {
            if (_usersContext.Users == null)
            {
                return NotFound();
            }
            return await _usersContext.Users.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsersByID(int id)
        {
            if (_usersContext.Users == null)
            {
                return NotFound();
            }
            var user = await _usersContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _usersContext.Users.Add(user);
            await _usersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsersByID), new { id = user.ID }, user);
        }

        [HttpPut("id")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
        if (id != user.ID)
        {
            return BadRequest();
        }
        _usersContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _usersContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw;
            }
            return Ok();

        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (_usersContext.Users == null)
            {
                return NotFound();
            }

            var user = await _usersContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            _usersContext.Users.Remove(user);
            await _usersContext.SaveChangesAsync();
            return Ok();

        }
    }
}
