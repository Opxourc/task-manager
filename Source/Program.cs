using task_manager.Models;
using task_manager.Shared;
using task_manager.Database;
using TaskModel = task_manager.Models.Task;
using TaskStatusModel = task_manager.Models.TaskStatus;
using System.Text.Json;

class Program
{
    private static List<TaskModel> tasks = new();
    private static int nextId = 1;
    private static string fileName = "Tasks.json";

    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     Welcome to Task Manager CLI        ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // Load tasks from file, or initialize empty list if file doesn't exist
        var loadedTasks = JSONManager.Deserialize<List<TaskModel>>(fileName);
        tasks = loadedTasks ?? new List<TaskModel>();

        bool isRunning = true;
        while (isRunning)
        {
            DisplayMenu();
            string? input = Console.ReadLine();
            isRunning = HandleCommand(input);
        }

        Console.WriteLine("\nThank you for using Task Manager. Goodbye!");
        JSONManager.Serialize(fileName, tasks, jsonOptions);
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
                ConsoleUtility.ColorConsoleText("❌ Invalid choice. Please try again.", ConsoleColor.Red);
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
            ConsoleUtility.ColorConsoleText("❌ Task title cannot be empty.", ConsoleColor.Red);
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
        ConsoleUtility.ColorConsoleText($"✅ Task added successfully! (ID: {newTask.Id})", ConsoleColor.Green);
    }

    static void ViewTasks()
    {
        Console.WriteLine("\n--- Your Tasks ---");

        if (tasks.Count == 0)
        {
            ConsoleUtility.ColorConsoleText("No tasks yet. Add one to get started!", ConsoleColor.Yellow);
            return;
        }

        Console.WriteLine($"\nTotal tasks: {tasks.Count}\n");
        foreach (var task in tasks)
        {
            string statusIcon = task.Status == TaskStatusModel.Completed ? "🟢" : "🔴";
            Console.WriteLine($"\nTask ID: {task.Id}");
            Console.WriteLine($"    Status: {statusIcon} {task.Status}");
            Console.WriteLine($"    Created at: {task.CreatedAt}");

            if (!string.IsNullOrEmpty(task.Description))
                Console.WriteLine($"    Description: {task.Description}");
        }
    }
}