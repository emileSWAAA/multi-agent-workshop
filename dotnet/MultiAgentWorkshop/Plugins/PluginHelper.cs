namespace MultiAgentWorkshop.Plugins
{
    internal static class PluginHelper
    {
        internal static void WriteToConsole(string methodName, params string[] parameters)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{methodName} was called with parameters: ");
            Console.ForegroundColor = ConsoleColor.White;
            if (parameters.Length == 0)
            {
                Console.WriteLine("No parameters provided.");
            }
            else
            {
                Console.WriteLine(string.Join(", ", parameters));
            }

            Console.ResetColor();
        }
    }
}
