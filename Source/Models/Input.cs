using ConsoleUtility = task_manager.Shared.ConsoleUtility;
using TaskStatus = task_manager.Models.TaskStatus;

namespace task_manager.Models
{
    public static class Input
    {
        /// <summary>
        /// Gets the input from the user that's needed for adding a task to the TaskList. 
        /// </summary>
        public static (string title, string description)? ForAddingTask()
        {
            Console.WriteLine("\n--- Add New Task ---");

            // Get the title, checking for invalid inputs
            Console.Write("Enter task title: ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                ConsoleUtility.ColorConsoleText(
                    "❌ Task title cannot be empty.",
                     ConsoleColor.Red
                );
                return null;
            }

            // Get a optional description, nulls and whitespaces are accepted here
            Console.Write("Enter task description (optional, press Enter to skip): ");
            string? description = Console.ReadLine() ?? string.Empty;

            return (title, description);
        }

        /// <summary>
        /// Gets the input from the user that's needed for removing a task from the TaskList.
        /// If no tasks are in the list that can be removed, then a quick return and hint is printed instead.
        /// </summary>
        /// <returns>
        /// Index of the task to be removed. Null if the input failed.
        /// </returns>
        public static int? ForRemovingTask()
        {
            // Check if there's any task to actually remove
            if (TaskList.GetAllTasks().Count <= 0)
            {
                ConsoleUtility.ColorConsoleText(
                    "There's no task that can be removed.",
                     ConsoleColor.DarkYellow
                );
                return null;
            }

            Console.WriteLine("\n--- Remove Task ---");

            // Get the index of the task, handing failed parses in the process
            // For convience, show the tasks and their indexes
            Console.Write("Enter the index the task you want to remove.");
            ForViewingTasks();

            if (!int.TryParse(Console.ReadLine(), out int index))
            {
                ConsoleUtility.ColorConsoleText(
                    "❌ Please enter a valid task index.",
                    ConsoleColor.Red
                );
                return null;
            }

            return index;
        }


        /// <summary>
        /// Prints the entire list of tasks in a format with their information.
        /// If no tasks are in the list, a quick return and hint is printed instead.
        /// </summary>
        public static void ForViewingTasks()
        {
            // Check if there's any task to actually view
            int taskCount = TaskList.GetAllTasks().Count;
            if (taskCount <= 0)
            {
                ConsoleUtility.ColorConsoleText(
                    "There's no tasks to view.",
                    ConsoleColor.DarkYellow
                );
                return;
            }

            Console.WriteLine("\n--- Your Tasks ---");

            // Print the list in a format
            var list = TaskList.GetAllTasks();
            Console.WriteLine($"\n{taskCount} Tasks\n");
            for (int i = 0; i < taskCount; i++)
            {
                Task task = list[i];
                string statusIcon = task.Status == TaskStatus.Completed ? "🟢" : "🔴";

                Console.WriteLine($"\n{i}.");
                Console.WriteLine($"    Status: {statusIcon} {task.Status}");
                Console.WriteLine($"    Created at: {task.CreatedAt}");

                if (!string.IsNullOrEmpty(task.Description))
                    Console.WriteLine($"    Description: {task.Description}");
            }
        }
    }
}