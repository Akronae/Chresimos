using System;

namespace Chresimos.Core.Utils
{
    public static class LogUtils
    {
        private static Action<string> LogAction = Console.WriteLine;
        private static Action<string> ErrorAction = Console.WriteLine;
        private static Action<string> WarnAction = Console.WriteLine;
        private static readonly bool UseColor;

        static LogUtils ()
        {
            UseColor = false;
            var consoleUndefined = Equals(typeof(Console), Type.Missing);
            if (consoleUndefined) return;
            var colorDefined = typeof(Console).GetProperty(nameof(Console.ForegroundColor)) != null;
            if (colorDefined) UseColor = true;
        }

        public static void Initialize (Action<string> logAction, Action<string> errorAction, Action<string> warnAction)
        {
            LogAction = logAction;
            ErrorAction = errorAction;
            WarnAction = warnAction;
        }

        public static void Initialize (Action<string> writeAction)
        {
            Initialize(writeAction, writeAction, writeAction);
        }

        public static void Log (string message)
        {
            if (LogAction is null) throw new Exception($"{nameof(LogAction)} is null, can't log");

            LogAction.Invoke(message);
        }

        public static void Error (string message)
        {
            if (ErrorAction is null) throw new Exception($"{nameof(ErrorAction)} is null, can't log");

            if (UseColor)
            {
                Output(ErrorAction, message, ConsoleColor.Red);
            }
            else
            {
                ErrorAction.Invoke(message);
            }
        }

        public static void Warn (string message)
        {
            if (WarnAction is null) throw new Exception($"{nameof(WarnAction)} is null, can't log");

            if (UseColor)
            {
                Output(WarnAction, message, ConsoleColor.DarkYellow);
            }
            else
            {
                WarnAction.Invoke(message);
            }
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
    }
}