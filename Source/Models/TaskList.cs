namespace task_manager.Models
{
    public class TaskList()
    {
        private static readonly List<Task> _tasks = new();

        /// <summary>
        /// Contains the required values for calling the AddTask method.
        /// </summary>
        public record AddTaskArg
        (
            string Description,
            string Title
        );

        /// <summary>
        /// Load the list either with premade tasks or a empty list.
        /// </summary>
        public static void LoadList(IEnumerable<Task> tasks)
        {
            _tasks.Clear();
            _tasks.AddRange(tasks);
        }

        /// <summary>
        /// Add a task to the list of tasks.
        /// </summary>
        public static void AddTask(AddTaskArg arg)
        {
            var newTask = new Task
            {
                Title = arg.Title,
                CreatedAt = DateTime.Now,
                Status = TaskStatus.Pending,
                Description = arg.Description,
            };

            _tasks.Add(newTask);
            Shared.ConsoleUtility.ColorConsoleText(
                $"✅ Task added successfully!",
                ConsoleColor.Green
            );
        }

        /// <summary>
        /// Remove a task from the list.
        /// </summary>
        public static void RemoveTask(int index)
        {
            Task? task = _tasks[index];

            if (task == null)
            {
                Shared.ConsoleUtility.ColorConsoleText(
                    $"❌ No task with an index of {index} was found. No deletions were made.",
                    ConsoleColor.Red
                );
                return;
            }

            _tasks.Remove(task);
            Shared.ConsoleUtility.ColorConsoleText(
                $"✅ Task removed successfully!",
                ConsoleColor.Green
            );
        }

        public static Task? GetATask(int index)
        {
            return _tasks[index];
        }

        public static IReadOnlyList<Task> GetAllTasks()
        {
            return _tasks;
        }
    }
}