using System;

namespace FrostRaven.Core
{
    public static class Log
    {
#if DEBUG
        public static bool IsEnabled = true;
#else
        public static bool IsEnabled = false;
#endif

        public static string DefaultSender = "GameCore";

        private static void write(string message, string sender)
        {
            if (!IsEnabled || String.IsNullOrEmpty(message))
            {
                return;
            }
            if (String.IsNullOrEmpty(sender))
            {
                sender = DefaultSender;
            }
            Console.WriteLine($"[{DateTime.Now.ToString("T")}] {sender}: \"{message}\"");
        }

        public static void Trace<T>(T message, string sender = "")
        {
            if(message == null)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            write(message.ToString(), sender);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info<T>(T message, string sender = "")
        {
            if (message == null)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            write(message.ToString(), sender);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warn<T>(T message, string sender = "")
        {
            if (message == null)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            write(message.ToString(), sender);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error<T>(T message, string sender = "")
        {
            if (message == null)
            {
                return;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            if(message is Exception exeption)
            {
                write(exeption.Message, sender);
            }
            else
            {
                write(message.ToString(), sender);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
