namespace task_manager.Models;

/// <summary>
/// Represents a single task in the task manager.
/// </summary>
public class Task
{
    /// <summary>
    /// Unique identifier for the task
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title/name of the task
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Detailed description of the task
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the task (Pending or Completed)
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.Pending;

    /// <summary>
    /// When the task was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"[{Id}] {Title} - {Status} (Created: {CreatedAt:yyyy-MM-dd HH:mm})";
    }
}

/// <summary>
/// Represents the possible statuses for a task
/// </summary>
public enum TaskStatus
{
    Pending,
    Completed
}
