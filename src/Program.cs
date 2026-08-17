using task_manager.Models;
using TaskModel = task_manager.Models.Task;
using TaskStatusModel = task_manager.Models.TaskStatus;

class Program
{
    private static List<TaskModel> tasks = new();
    private static int nextId = 1;

    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     Welcome to Task Manager CLI        ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        bool isRunning = true;
        while (isRunning)
        {
            DisplayMenu();
            string? input = Console.ReadLine();

            isRunning = HandleCommand(input);
        }

        Console.WriteLine("\nThank you for using Task Manager. Goodbye!");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n--- Main Menu ---");
        Console.WriteLine("1. Add a new task");
        Console.WriteLine("2. View all tasks");
        Console.WriteLine("3. Exit");
        Console.Write("\nEnter your choice (1-3): ");
    }

    static bool HandleCommand(string? input)
    {
        switch (input)
        {
            case "1":
                AddTask();
                return true;
            case "2":
                ViewTasks();
                return true;
            case "3":
                return false;
            default:
                Console.WriteLine("Invalid choice. Please try again.");
                return true;
        }
    }

    static void AddTask()
    {
        Console.WriteLine("\n--- Add New Task ---");

        Console.Write("Enter task title: ");
        string? title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("❌ Task title cannot be empty.");
            return;
        }

        Console.Write("Enter task description (optional, press Enter to skip): ");
        string? description = Console.ReadLine();

        var newTask = new TaskModel
        {
            Id = nextId++,
            Title = title,
            Description = description ?? string.Empty,
            Status = TaskStatusModel.Pending,
            CreatedAt = DateTime.Now
        };

        tasks.Add(newTask);
        Console.WriteLine($"✓ Task added successfully! (ID: {newTask.Id})");
    }

    static void ViewTasks()
    {
        Console.WriteLine("\n--- Your Tasks ---");

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet. Add one to get started!");
            return;
        }

        foreach (var task in tasks)
        {
            string statusIcon = task.Status == TaskStatusModel.Completed ? "✓" : "○";
            Console.WriteLine($"{statusIcon} {task}");
            if (!string.IsNullOrEmpty(task.Description))
            {
                Console.WriteLine($"   Description: {task.Description}");
            }
        }

        Console.WriteLine($"\nTotal tasks: {tasks.Count}");
    }
}