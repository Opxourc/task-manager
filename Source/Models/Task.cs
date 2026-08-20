namespace task_manager.Models
{
    /// <summary>
    /// Represents a single task in the task manager.
    /// </summary>
    public class Task
    {
        public required string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Represents the possible statuses for a task
    /// </summary>
    public enum TaskStatus
    {
        Pending,
        Completed
    }
}
