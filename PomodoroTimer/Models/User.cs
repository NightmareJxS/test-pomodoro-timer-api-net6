using System;
using System.Collections.Generic;

namespace PomodoroTimer.Models
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
        }

        public string Id { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? TimeFocusToday { get; set; }
        public int? TimeFocusThisWeek { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
