using FaintAlertAPI.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FaintAlertAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Password = "password123" },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Password = "password456" }
        };

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User newUser)
        {
            if(users.Any(h => h.Id == newUser.Id))
            {
                return Conflict("User with the same ID already exists.");
            }
            users.Add(newUser);
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            return NoContent();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            users.Remove(user);
            return NoContent();
        }
    }
}
