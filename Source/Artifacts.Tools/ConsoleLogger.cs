using System;

namespace Dolittle.Artifacts.Tools
{
    internal static class ConsoleLogger
    {
        internal static void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Info: " + message);
        }

        internal static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + message);
        }

        internal static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("Error: " + message);
        }
    }
}