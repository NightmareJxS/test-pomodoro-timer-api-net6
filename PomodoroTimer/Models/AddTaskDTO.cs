using System.ComponentModel.DataAnnotations;

namespace PomodoroTimer.Models
{
    public class AddTaskDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CompleteTime { get; set; }
    }
}
