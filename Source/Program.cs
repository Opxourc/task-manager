using JSONManager = task_manager.Shared.JSONManager;
using ConsoleUtility = task_manager.Shared.ConsoleUtility;
using Task = task_manager.Models.Task;
using TaskList = task_manager.Models.TaskList;
using Input = task_manager.Models.Input;
using Json = System.Text.Json;

class Program
{
    private const string fileName = "Tasks.json";

    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     Welcome to Task Manager CLI        ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Attempt to get saved JSON file and load the data into memory
        // If no data exists, use an empty table instead
        var jsonOptions = new Json.JsonSerializerOptions { WriteIndented = true };
        var loadedTasks = JSONManager.Deserialize<List<Task>>(fileName);
        TaskList.LoadList(loadedTasks ?? Enumerable.Empty<Task>());

        // Display menu and grab input in what the user wants to do
        // Keep the menu running until isRunning becomes false by a command
        bool isRunning = true;
        while (isRunning)
        {
            DisplayMenu();
            string? input = Console.ReadLine();
            isRunning = HandleCommand(input);
        }

        // Save all tasks currently in memory
        var tasks = TaskList.GetAllTasks();
        JSONManager.Serialize(fileName, tasks, jsonOptions);
        Console.WriteLine("\nThank you for using the Task Manager. Goodbye!");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n--- Main Menu ---");
        Console.WriteLine("1. Add a new task");
        Console.WriteLine("2. Remove a task");
        Console.WriteLine("3. View all tasks");
        Console.WriteLine("4. Exit");
        Console.Write("\nEnter your choice (1-4): ");
    }

    static bool HandleCommand(string? input)
    {
        switch (input)
        {
            case "1": // Add a task
                {
                    // Get values of inputs for adding the task
                    var inputs = Input.ForAddingTask();
                    if (inputs == null)
                        return true;

                    // Add the new task to the list
                    var arguements = new TaskList.AddTaskArg(inputs.Value.description, inputs.Value.title);
                    TaskList.AddTask(arguements);

                    return true;
                }

            case "2": // Remove a task
                {
                    // Get index that is for the task the user wants to remove
                    var index = Input.ForRemovingTask();
                    if (index == null)
                        return true;

                    // Remove the task from the list
                    TaskList.RemoveTask(index.Value);
                    return true;
                }

            case "3": // View a task
                Input.ForViewingTasks();
                return true;

            case "4": // Exit program
                return false;

            default:
                ConsoleUtility.ColorConsoleText(
                    "❌ Invalid choice. Please try again.",
                    ConsoleColor.Red
                );
                return true;
        }
    }
}