using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PomodoroTimer.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public int? Status { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
