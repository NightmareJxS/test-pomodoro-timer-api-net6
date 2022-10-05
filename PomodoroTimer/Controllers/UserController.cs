using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PomodoroTimer.Models;

namespace PomodoroTimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PomodoroTimerContext _context;

        public UserController(PomodoroTimerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUser()
        {
            try
            {
                if (_context.Users == null) // No user in Database
                {
                    return NotFound();
                }

                var users = await _context.Users.ToListAsync();

                // manually change each password to Empty (bad code)(don't know any other way)
                foreach (var user in users)
                {
                    user.Password = string.Empty;
                }

                return Ok(users);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUserId(string Id)
        {
            try
            {
                if (_context.Users == null) // No user in Database
                {
                    return NotFound();
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
                user.Password = "*******";

                if (user == null) // Not found user in Database
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserLoginDTO request)
        {
            try
            {
                var user = await _context.Users
                .Where(u => u.Id == request.Id && u.Password == request.Password)
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync();

                if (user == null) // Not found user
                {
                    return NotFound("Incorrect Email or Password");
                }

                return Ok(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateNewUser(CreateUserDTO request)
        {
            try
            {
                var user = await _context.Users.FindAsync(request.Id);
                if (user != null)
                {
                    return BadRequest("Duplicate userID");
                }

                var newUser = new User
                {
                    Id = request.Id,
                    Email = request.Email,
                    Password = request.Password,
                    TimeFocusToday = 0,
                    TimeFocusThisWeek = 0
                };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Created("", newUser); // return created User
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
