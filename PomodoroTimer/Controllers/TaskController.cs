using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PomodoroTimer.Models;
using Task = PomodoroTimer.Models.Task;

namespace PomodoroTimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly PomodoroTimerContext _context;

        public TaskController(PomodoroTimerContext context)
        {
            _context = context;
        }

        [HttpPost("addUserTask")]
        public async Task<ActionResult<Task>> AddNewTask(AddTaskDTO request)
        {
            try
            {
                var user = await _context.Users.FindAsync(request.UserId);

                if (user == null) // Not found UserId in Database
                {
                    return NotFound("UserID not found");
                }

                var newTask = new Task
                {
                    Name = request.Name,
                    Duration = request.Duration,
                    StartTime = request.StartTime,
                    CompleteTime = request.CompleteTime,
                    Status = 1,
                    User = user
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();

                return Created("", newTask);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
