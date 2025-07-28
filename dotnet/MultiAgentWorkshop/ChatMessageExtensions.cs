using Microsoft.SemanticKernel;

namespace MultiAgentWorkshop
{
    internal static class ChatMessageExtensions
    {
        internal static void WriteAgentMessage(this ChatMessageContent? message)
        {
            if (message == null)
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{message.AuthorName}: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message.Content);

            Console.ResetColor();
        }
    }
}
