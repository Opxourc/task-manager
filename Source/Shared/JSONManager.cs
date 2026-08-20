namespace task_manager.Shared
{
    using System.Text.Json;

    public class JSONManager
    {
        /// <summary>
        /// Serializes the provided data into a JSON file format.
        /// The JSON file will reside within the same directory as the JSONManager.
        /// </summary>
        /// <returns>
        /// Represents the success or failure of the serialization.
        /// </returns>
        public static bool Serialize<T>(string filePath, T data, JsonSerializerOptions? options)
        {
            try
            {
                string fullFilePath = Path.GetFullPath(filePath);
                string? directoryPath = Path.GetDirectoryName(fullFilePath);

                if (directoryPath is not null)
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var json = JsonSerializer.Serialize(data, options);
                File.WriteAllText(fullFilePath, json);

                ConsoleUtility.ColorConsoleText(
                    $"Successfully saved tasks to {fullFilePath}",
                    ConsoleColor.Green
                );

                return true;
            }
            catch (Exception ex)
            {
                ConsoleUtility.ColorConsoleText(
                    $"Failed to serialize task data. {ex.Message}",
                    ConsoleColor.Red
                );

                return false;
            }
        }

        /// <summary>
        /// Deserializes the provided JSON file name to a typed object.
        /// If the file does not exist, returns a default empty instance.
        /// </summary>
        /// <returns>
        /// The deserialized object, or a default instance if file doesn't exist or deserialization fails.
        /// </returns>
        public static T? Deserialize<T>(string filePath)
        {
            // If file doesn't exist, return null (caller should handle with default)
            if (!File.Exists(filePath))
            {
                return default;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonException exception)
            {
                ConsoleUtility.ColorConsoleText(
                    $"Failed to deserialize task data. {exception.Message}",
                    ConsoleColor.Red
                );

                return default;
            }
            catch (IOException exception)
            {
                ConsoleUtility.ColorConsoleText(
                    $"Failed to read task data from file. {exception.Message}",
                    ConsoleColor.Red
                );

                return default;
            }
        }
    }
}