using System;

namespace Chresimos.Core.Utils
{
    public static class LogUtils
    {
        private static Action<string> LogAction = Console.WriteLine;
        private static Action<string> ErrorAction = Console.WriteLine;
        private static Action<string> WarnAction = Console.WriteLine;
        private static readonly bool UseColor;
        public static DateLogging DateLoggingMod;

        static LogUtils ()
        {
            UseColor = false;
            var consoleUndefined = Equals(typeof(Console), Type.Missing);
            if (consoleUndefined) return;
            var colorDefined = typeof(Console).GetProperty(nameof(Console.ForegroundColor)) != null;
            if (colorDefined) UseColor = true;
        }

        public static void Initialize (Action<string> logAction, Action<string> errorAction, Action<string> warnAction, DateLogging dateLogging)
        {
            LogAction = logAction;
            ErrorAction = errorAction;
            WarnAction = warnAction;
            DateLoggingMod = dateLogging;
        }

        public static void Initialize (Action<string> writeAction, DateLogging dateLogging = DateLogging.None)
        {
            Initialize(writeAction, writeAction, writeAction, dateLogging);
        }

        public static void Log (string message)
        {
            if (LogAction is null) throw new Exception($"{nameof(LogAction)} is null, can't log");

            Output(LogAction, message, Console.ForegroundColor);
        }

        public static void Error (string message)
        {
            if (ErrorAction is null) throw new Exception($"{nameof(ErrorAction)} is null, can't log");

            Output(ErrorAction, message, ConsoleColor.Red);
        }

        public static void Warn (string message)
        {
            if (WarnAction is null) throw new Exception($"{nameof(WarnAction)} is null, can't log");

            Output(WarnAction, message, ConsoleColor.DarkYellow);
        }

        public static Exception Throw (Exception ex)
        {
            Error(ex.ToString());
            throw ex;
        }

        public static Exception Throw (string message)
        {
            throw Throw(new Exception(message));
        }

        private static void Output (Action<string> outputAction, string message, ConsoleColor color)
        {
            switch (DateLoggingMod)
            {
                case DateLogging.None:
                    break;
                case DateLogging.Time:
                    message = $"[{DateTime.Now:HH:mm:ss}] {message}";
                    break;
                case DateLogging.DateTime:
                    message = $"[{DateTime.Now:MM/dd/yyyy HH:mm:ss}] {message}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (!UseColor)
            {
                outputAction.Invoke(message);
                return;
            }

            var f = Console.ForegroundColor;
            Console.ForegroundColor = color;
            outputAction.Invoke(message);
            Console.ForegroundColor = f;
        }
        
        public enum DateLogging
        {
            None,
            Time,
            DateTime
        }
    }
}