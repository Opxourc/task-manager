namespace task_manager.Shared
{
    public class ConsoleUtility
    {
        /// <summary>
        /// Writes the provided text to the Console with it being colored in a specific color.
        /// When a custom color is applied, the ForegroundColor is reset back to being gray after the text has been printed.
        /// </summary>
        public static void ColorConsoleText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}